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
    public Int64 CompanyId { get; set; }
    public Int64 SaleId { get; set; }
}

public class DeleteSaleByIdCommandHandler : IRequestHandler<DeleteSaleByIdCommand, Response<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<SaleEntity> _repository;
    private readonly IMongoRepository<SaleHistoryEntity> _histRepository;

    public DeleteSaleByIdCommandHandler(IMapper mapper, IMongoRepository<SaleEntity> repository, IMongoRepository<SaleHistoryEntity> histRepository)
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
            var filter = request.SaleId.FindQueryBySaleId(request.CompanyId);
            await _repository.DeleteOneAsync(filter);

            await SaveHistory(request.SaleId, request.CompanyId);

            Log.Information($"Compra Excluida - {this.GetType().Name}");

            return new Response<Unit>(new Unit());
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }

    private async Task SaveHistory(Int64 saleId, Int64 comanyId)
    {
        string msg = "Compra excluida";

        await _histRepository.InsertOneAsync(new SaleHistoryEntity
        {
            CompanyId= comanyId,
            SaleId = saleId,
            Message = msg,
            UserName = "Usuário logado",
            Status = "Excluido"
        });
    }
}

