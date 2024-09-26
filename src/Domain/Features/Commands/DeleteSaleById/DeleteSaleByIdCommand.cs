using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions.Filters;
using Domain.Intefaces;
using Domain.Wrappers;
using MediatR;
using Serilog;

namespace Domain.Features.Commands.DeleteSaleById;

public class DeleteSaleByIdCommand : IRequest<Response<Unit>>
{
    public Int64 SaleId { get; set; }
}

public class DeleteSaleByIdCommandHandler : IRequestHandler<DeleteSaleByIdCommand, Response<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<SaleEntity> _repository;

    public DeleteSaleByIdCommandHandler(IMapper mapper, IMongoRepository<SaleEntity> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Response<Unit>> Handle(DeleteSaleByIdCommand request, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando - {this.GetType().Name}");

        try
        {
            var filter = request.SaleId.FindQueryBySaleId();
            await _repository.DeleteOneAsync(filter);

            Log.Information($"Compra Excluida - {this.GetType().Name}");

            return new Response<Unit>(new Unit());
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }
}

