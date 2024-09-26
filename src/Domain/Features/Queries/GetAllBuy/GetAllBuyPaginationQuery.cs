using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Extensions.Filters;
using Domain.Intefaces;
using MediatR;
using Serilog;

namespace Domain.Features.Queries;

public class GetAllBuyPaginationQuery : IPagination, IRequest<PaginationResult<BuyQueryContract>>
{
    public Int64? BuyId { get; set; }
    public string? CustomerName { get; set; }
    public string? ProductName { get; set; }
    public DateTime? BuyInititalDate { get; set; }
    public DateTime? BuyEndDate { get; set; }
    public int? Status { get; set; }

    public int? PageSize { get; set; }
    public int? Page { get; set; }
    public string? SortBy { get; set; }
    public string? OrderBy { get; set; }
}

public class GetAllBuyPaginationQueryHandler : IRequestHandler<GetAllBuyPaginationQuery, PaginationResult<BuyQueryContract>>
{
    private readonly IMongoRepository<BuyEntity> _repository;

    public GetAllBuyPaginationQueryHandler(IMongoRepository<BuyEntity> repository)
    {
        _repository = repository;
    }

    public async Task<PaginationResult<BuyQueryContract>> Handle(GetAllBuyPaginationQuery query, CancellationToken cancellationToken)
    {
        Log.Information($"Iniciando {this.GetType().Name}");

        try
        {
            var filter = query.Filter();

            var entities = await _repository.FilterPaginationBy(query.Page, query.PageSize, filter, query.SortBy, query.OrderBy);

            if (entities == null || !entities.Any())
                throw new ApiException("Dados não encontrados");

            var countDB = await _repository.CountDocuments(filter);
            var page = query.Page ?? 1;
            var pageSize = query.PageSize ?? 10;

            var result = entities.Select(x => new BuyQueryContract
            {
                BuyId = x.BuyId,
                Status = x.Status.ToEnum<BuyStatusEnum>(),
                TotalBuyPrice = x.TotalBuyPrice,
                Customer = new CustomerContract
                {
                    CustomerId = x.Customer.CustomerId,
                    Name = x.Customer.Name
                },
                Products = x.Products.Select(p => new ProductQueryContract
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Discount = p.Discount,
                    UnitPrice = p.UnitPrice,
                    Status = p.Status.ToEnum<BuyItemStatusEnum>()
                }).ToList()
            }).ToList();

            return new PaginationResult<BuyQueryContract>(page, pageSize, countDB, result);

        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }
}

