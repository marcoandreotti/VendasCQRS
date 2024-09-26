using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
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

    public UpdateSaleCommandHandler(IMapper mapper, IMongoRepository<SaleEntity> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Response<Unit>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando - {this.GetType().Name}");

        try
        {
            var entity = await ExistingEntity(request.SaleId);

            var id = entity.Id;

            entity = _mapper.Map<SaleEntity>(request);
            entity.Id = id;

            //Aqui validaremos e carregaremos o nome do Cliente caso exita
            entity.Customer.Name = await GetCostumerNameOnCRM(request.CustomerId);

            //Aqui validaremos os produtos e adicionaremos o nome caso exita
            entity.Products = await GetProductsNameOnCRM(entity.Products);

            await _repository.ReplaceOneAsync(entity);

            Log.Information($"Compra Alterada - {this.GetType().Name}");

            return new Response<Unit>(new Unit());
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
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

    private async Task<SaleEntity> ExistingEntity(Int64 saleId)
    {
        var entity = await _repository.FindOneAsync(saleId.FindBySaleId());

        if (entity == null)
            throw new ApiException($"Compra não encontrada - id {saleId}", true);

        return entity;
    }
}

