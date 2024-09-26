using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions.Filters;
using Domain.Intefaces;
using Domain.Wrappers;
using MediatR;
using Serilog;

namespace Domain.Features.Commands.DeleteBuyById;

public class DeleteBuyByIdCommand : IRequest<Response<Unit>>
{
    public Int64 BuyId { get; set; }
}

public class DeleteBuyByIdCommandHandler : IRequestHandler<DeleteBuyByIdCommand, Response<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<BuyEntity> _repository;
    private readonly IMongoRepository<BuyHistoryEntity> _histRepository;

    public DeleteBuyByIdCommandHandler(IMapper mapper, IMongoRepository<BuyEntity> repository, IMongoRepository<BuyHistoryEntity> histRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _histRepository = histRepository;
    }

    public async Task<Response<Unit>> Handle(DeleteBuyByIdCommand request, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando - {this.GetType().Name}");

        try
        {
            var filter = request.BuyId.FindQueryByBuyId();
            await _repository.DeleteOneAsync(filter);

            await SaveHistory(request.BuyId);

            Log.Information($"Compra Excluida - {this.GetType().Name}");

            return new Response<Unit>(new Unit());
        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }

    private async Task SaveHistory(Int64 buyId)
    {
        string msg = "Compra excluida";

        await _histRepository.InsertOneAsync(new BuyHistoryEntity
        {
            BuyId = buyId,
            Message = msg,
            UserName = "Usuário logado",
            Status = "Excluido"
        });
    }
}

