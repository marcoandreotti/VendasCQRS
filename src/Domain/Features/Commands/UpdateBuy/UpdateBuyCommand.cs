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

namespace Domain.Features.Commands.UpdateBuy;

public class UpdateBuyCommand : BuyUpdateContract, IRequest<Response<Unit>> { }

public class UpdateBuyCommandHandler : IRequestHandler<UpdateBuyCommand, Response<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<BuyEntity> _repository;
    private readonly IMongoRepository<BuyHistoryEntity> _histRepository;

    public UpdateBuyCommandHandler(IMapper mapper, IMongoRepository<BuyEntity> repository, IMongoRepository<BuyHistoryEntity> histRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _histRepository = histRepository;
    }

    public async Task<Response<Unit>> Handle(UpdateBuyCommand request, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando - {this.GetType().Name}");

        try
        {
            var entity = await ExistingEntity(request.BuyId);

            if (entity.Status == (int)BuyStatusEnum.CompraCancelada)
                throw new ApiException("Compra cancelada, não é possível alterar");

            var id = entity.Id;
            bool statusModified = (int)request.Status != entity.Status;
            bool stautsProductModified = false;
            bool productModified = false;

            if (request.CustomerId != entity.Customer.CustomerId)
                entity.Customer.CustomerId = request.CustomerId;

            if (request.BuyDate != entity.BuyDate)
                entity.BuyDate = request.BuyDate;

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
                entity.Status = (int)BuyStatusEnum.CompraAlterada;
                await SaveHistory(entity, $"Produtos alterados");
            }

            if (stautsProductModified)
            {
                //Verificando se todos os itens foram cancelados
                if (entity.Products.Any(x => x.Status != (int)BuyItemStatusEnum.ItemCancelado))
                {
                    entity.Status = (int)BuyStatusEnum.CompraAlterada;
                    await SaveHistory(entity, $"Produtos cancelados");
                }
                else
                {
                    //Cancelando a compra caso todos os itens estejam cancelados
                    entity.Status = (int)BuyStatusEnum.CompraCancelada;
                    await SaveHistory(entity, "Todos os produtos foram cancelados");
                }
            }

            await _repository.ReplaceOneAsync(entity);

            //Independente dos produtos se houver alteração do status tb, na entidade principal será lançado como histórico
            if (statusModified)
                await SaveHistory(entity);

            Log.Information($"Compra Alterada - {this.GetType().Name}");

            return new Response<Unit>(new Unit());
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }
    private async Task SaveHistory(BuyEntity entity, string msgStatus = "")
    {
        string msg = "Status da compra alterado";

        if (!string.IsNullOrWhiteSpace(msgStatus)) msg = string.Concat(msg, " - ", msgStatus);

        await _histRepository.InsertOneAsync(new BuyHistoryEntity
        {
            BuyId = entity.BuyId,
            Message = msg,
            UserName = "Usuário logado",
            BuyDate = entity.BuyDate,
            Status = ((BuyStatusEnum)entity.Status).GetDisplayName()
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

    private async Task<BuyEntity> ExistingEntity(Int64 buyId)
    {
        var entity = await _repository.FindOneAsync(buyId.FindByBuyId());

        if (entity == null)
            throw new ApiException($"Compra não encontrada - id {buyId}", true);

        return entity;
    }
}

