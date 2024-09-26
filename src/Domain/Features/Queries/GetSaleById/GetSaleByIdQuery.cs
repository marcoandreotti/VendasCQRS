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

namespace Domain.Features.Queries.GetSaleById;

public class GetSaleByIdQuery : IRequest<Response<SaleQueryContract>>
{
    public Int64 SaleId { get; set; }
}

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Response<SaleQueryContract>>
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<SaleEntity> _repository;

    public GetSaleByIdQueryHandler(IMapper mapper, IMongoRepository<SaleEntity> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Response<SaleQueryContract>> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando {this.GetType().Name}");

        try
        {
            var filter = query.SaleId.FindQueryBySaleId();
            var entity = await _repository.FindOneAsync(filter);

            if (entity == null)
                throw new ApiException("Compra não encontrada");

            var result = _mapper.Map<SaleEntity, SaleQueryContract>(entity);

            return new Response<SaleQueryContract>(result);
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }
}

