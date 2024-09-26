using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions.Filters;
using Domain.Intefaces;
using Domain.Wrappers;
using MediatR;
using Serilog;

namespace Domain.Features.Queries.GetBuyById;

public class GetBuyByIdQuery : IRequest<Response<BuyQueryContract>>
{
    public Int64 BuyId { get; set; }
}

public class GetBuyByIdQueryHandler : IRequestHandler<GetBuyByIdQuery, Response<BuyQueryContract>>
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<BuyEntity> _repository;

    public GetBuyByIdQueryHandler(IMapper mapper, IMongoRepository<BuyEntity> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Response<BuyQueryContract>> Handle(GetBuyByIdQuery query, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando {this.GetType().Name}");

        try
        {
            var filter = query.BuyId.FindQueryByBuyId();
            var entity = await _repository.FindOneAsync(filter);

            if (entity == null)
                throw new ApiException("Compra não encontrada");

            var result = _mapper.Map<BuyEntity, BuyQueryContract>(entity);

            return new Response<BuyQueryContract>(result);
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }
}

