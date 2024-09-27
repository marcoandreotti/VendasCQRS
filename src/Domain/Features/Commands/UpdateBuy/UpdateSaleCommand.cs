using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Extensions.Filters;
using Domain.Intefaces;
using Domain.Wrappers;
using MediatR;
using Serilog;

namespace Domain.Features.Commands.UpdateSale;

public class UpdateSaleCommand : SaleUpdateContract, IRequest<Response<Unit>> { }

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Response<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<SaleEntity> _repository;
    private readonly IMongoRepository<SaleHistoryEntity> _histRepository;

    public UpdateSaleCommandHandler(IMapper mapper, IMongoRepository<SaleEntity> repository, IMongoRepository<SaleHistoryEntity> histRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _histRepository = histRepository;
    }

    public async Task<Response<Unit>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando - {this.GetType().Name}");

        try
        {
            var entity = await ExistingEntity(request.SaleId, request.CompanyId);

            if (entity.Status == (int)SaleStatusEnum.CompraCancelada)
                throw new ApiException("Compra cancelada, não é possível alterar");

            var id = entity.Id;
            bool statusModified = (int)request.Status != entity.Status;
            bool stautsProductModified = false;
            bool productModified = false;

            entity.Status = (int)request.Status;

            if (request.CustomerId != entity.Customer.CustomerId)
                entity.Customer.CustomerId = request.CustomerId;

            if (request.SaleDate != entity.SaleDate)
                entity.SaleDate = request.SaleDate;

            foreach (var prod in request.Products)
            {
                ProductEntity prodEntity = entity.Products.FirstOrDefault(x => x.ProductId == prod.ProductId);
                if (prodEntity != null)
                {
                    if (prodEntity.UnitPrice != prod.UnitPrice || prodEntity.Quantity != prod.Quantity || prodEntity.Discount != prod.Discount)
                    {
                        prodEntity.UnitPrice = prod.UnitPrice;
                        prodEntity.Quantity = prod.Quantity;
                        prodEntity.Discount = prod.Discount;
                        prodEntity.TotalPrice = prod.Quantity * (prod.UnitPrice - prod.Discount);
                        productModified = true;
                    }

                    if ((int)prod.Status > 0 && prodEntity.Status != (int)prod.Status)
                    {
                        stautsProductModified = true;
                        prodEntity.Status = (int)prod.Status;
                    }
                }
                else
                {
                    var prodE = _mapper.Map<ProductEntity>(prod);
                    entity.Products.Append(prodE);
                    productModified = true;
                }
            }

            //Aqui validaremos e carregaremos o nome do Cliente caso exita
            entity.Customer.Name = await GetCostumerNameOnCRM(request.CustomerId);

            //Aqui validaremos os produtos e adicionaremos o nome caso exita
            entity.Products = await GetProductsNameOnCRM(entity.Products);

            if (productModified)
            {
                entity.Status = (int)SaleStatusEnum.CompraAlterada;
                await SaveHistory(entity, $"Produtos alterados");
            }

            if (stautsProductModified)
            {
                //Verificando se todos os itens foram cancelados
                if (entity.Products.Any(x => x.Status != (int)SaleItemStatusEnum.ItemCancelado))
                {
                    entity.Status = (int)SaleStatusEnum.CompraAlterada;
                    await SaveHistory(entity, $"Produtos cancelados");
                }
                else
                {
                    //Cancelando a compra caso todos os itens estejam cancelados
                    entity.Status = (int)SaleStatusEnum.CompraCancelada;
                    await SaveHistory(entity, "Todos os produtos foram cancelados");
                }
            }

            await _repository.ReplaceOneAsync(entity);

            //Independente dos produtos se houver alteração do status tb, na entidade principal será lançado como histórico
            if (statusModified)
                await SaveHistory(entity, entity.Status.ToEnum<SaleStatusEnum>().GetDisplayName());

            Log.Information($"Compra Alterada - {this.GetType().Name}");

            return new Response<Unit>(new Unit());
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }
    private async Task SaveHistory(SaleEntity entity, string msgStatus = "")
    {
        string msg = "Status da compra alterado";

        if (!string.IsNullOrWhiteSpace(msgStatus)) msg = string.Concat(msg, " - ", msgStatus);

        await _histRepository.InsertOneAsync(new SaleHistoryEntity
        {
            CompanyId = entity.CompanyId,
            SaleId = entity.SaleId,
            Message = msg,
            UserName = "Usuário logado",
            SaleDate = entity.SaleDate,
            Status = ((SaleStatusEnum)entity.Status).GetDisplayName()
        });
    }

    private async Task<string> GetCostumerNameOnCRM(long costumerId)
    {
        if (costumerId == 99 || costumerId == 0)
            throw new ApiException("Cliente não localizado no CRM", true);

        return $"Nome do CLiente-{costumerId}";
    }

    private async Task<IEnumerable<ProductEntity>> GetProductsNameOnCRM(IEnumerable<ProductEntity> products)
    {
        if (products == null || !products.Any())
            throw new ApiException("Produtos não informados", true);

        foreach (var prod in products)
        {
            if (prod.ProductId == 99 || prod.ProductId == 0)
                throw new ApiException($"Produto não localizado no CRM - id {prod.ProductId}", true);

            prod.Name = $"Nome do Produto-{prod.ProductId}";
        }

        return products;
    }

    private async Task<SaleEntity> ExistingEntity(Int64 saleId, Int64 companyId)
    {
        var entity = await _repository.FindOneAsync(saleId.FindBySaleIds(companyId));

        if (entity == null)
            throw new ApiException($"Compra não encontrada - id {saleId}", true);

        return entity;
    }
}

