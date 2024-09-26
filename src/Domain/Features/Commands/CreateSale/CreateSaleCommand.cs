using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
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

    public CreateSaleCommandHandler(IMapper mapper, IMongoRepository<SaleEntity> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Response<Unit>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando {this.GetType().Name}");

        try
        {
            await ExistsEntity(request.SaleId);

            var entity = _mapper.Map<SaleEntity>(request);
            await _repository.InsertOneAsync(entity);
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
        return new Response<Unit>(new Unit());
    }

    private async Task ExistsEntity(Int64 saleId)
    {
        var entity = await _repository.FindOneAsync(saleId.FindBySaleId());

        if (entity != null)
            throw new ApiException("Venda já existe na base de dados", true);
    }
}

