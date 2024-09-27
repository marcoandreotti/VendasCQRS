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

public class GetAllSalePaginationQuery : IPagination, IRequest<PaginationResult<SaleQueryContract>>
{
    public Int64? SaleId { get; set; }
    public string? CustomerName { get; set; }
    public string? ProductName { get; set; }
    public DateTime? SaleInititalDate { get; set; }
    public DateTime? SaleEndDate { get; set; }
    public int? Status { get; set; }

    public int? PageSize { get; set; }
    public int? Page { get; set; }
    public string? SortBy { get; set; }
    public string? OrderBy { get; set; }
}

public class GetAllSalePaginationQueryHandler : IRequestHandler<GetAllSalePaginationQuery, PaginationResult<SaleQueryContract>>
{
    private readonly IMongoRepository<SaleEntity> _repository;

    public GetAllSalePaginationQueryHandler(IMongoRepository<SaleEntity> repository)
    {
        _repository = repository;
    }

    public async Task<PaginationResult<SaleQueryContract>> Handle(GetAllSalePaginationQuery query, CancellationToken cancellationToken)
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

            var result = entities.Select(x => new SaleQueryContract
            {
                SaleId = x.SaleId,
                Status = x.Status.ToEnum<SaleStatusEnum>(),
                TotalSalePrice = x.TotalSalePrice,
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
                    Status = p.Status.ToEnum<SaleItemStatusEnum>()
                }).ToList()
            }).ToList();

            return new PaginationResult<SaleQueryContract>(page, pageSize, countDB, result);

        }
        catch (Exception e)
        {
            throw new ApiException(e.Message, true);
        }
    }
}

