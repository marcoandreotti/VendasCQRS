using AutoMapper;
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
    private readonly IMongoRepository<SalesHistoryEntity> _histRepository;

    public DeleteSaleByIdCommandHandler(IMapper mapper, IMongoRepository<SaleEntity> repository, IMongoRepository<SalesHistoryEntity> histRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _histRepository = histRepository;
    }

    public async Task<Response<Unit>> Handle(DeleteSaleByIdCommand request, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando - {this.GetType().Name}");

        try
        {
            var filter = request.SaleId.FindQueryBySaleId();
            await _repository.DeleteOneAsync(filter);

            await SaveSalesHistory(request.SaleId);

            Log.Information($"Compra Excluida - {this.GetType().Name}");

            return new Response<Unit>(new Unit());
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }

    private async Task SaveSalesHistory(Int64 saleId)
    {
        string msg = "Compra excluida";

        await _histRepository.InsertOneAsync(new SalesHistoryEntity
        {
            SaleId = saleId,
            Message = msg,
            UserName = "Usuário logado",
            Status = "Excluido"
        });
    }
}

