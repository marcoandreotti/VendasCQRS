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

namespace Domain.Features.Commands.CreateSale;

public class CreateSaleCommand : SaleContract, IRequest<Response<Unit>> { }

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Response<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<SaleEntity> _repository;
    private readonly IMongoRepository<SaleHistoryEntity> _histRepository;

    public CreateSaleCommandHandler(IMapper mapper, IMongoRepository<SaleEntity> repository, IMongoRepository<SaleHistoryEntity> histRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _histRepository = histRepository;
    }

    public async Task<Response<Unit>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando - {this.GetType().Name}");

        try
        {
            await ExistsEntity(request.SaleId, request.CompanyId);

            var entity = _mapper.Map<SaleEntity>(request);

            //Aqui validaremos e carregaremos o nome do Cliente caso exita
            entity.Customer.Name = await GetCostumerNameOnCRM(request.CustomerId);

            //Aqui validaremos os produtos e adicionaremos o nome caso exita
            entity.Products = await GetProductsNameOnCRM(entity.Products);

            await _repository.InsertOneAsync(entity);

            await SaveHistory(entity);

            Log.Information($"Compra Criada - {this.GetType().Name}");

        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
        return new Response<Unit>(new Unit());
    }

    private async Task SaveHistory(SaleEntity entity)
    {
        string msg = "...";

        await _histRepository.InsertOneAsync(new SaleHistoryEntity
        {
            CompanyId = entity.CompanyId,
            SaleId = entity.SaleId,
            Message = msg,
            UserName = "Usuário logado",
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

    private async Task ExistsEntity(Int64 saleId, Int64 companyId)
    {
        var entity = await _repository.FindOneAsync(saleId.FindBySaleIds(companyId));

        if (entity != null)
            throw new ApiException("Venda já existe na base de dados", true);
    }
}

