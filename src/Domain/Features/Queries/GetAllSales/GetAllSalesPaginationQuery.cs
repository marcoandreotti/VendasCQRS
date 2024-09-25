using Domain.Contracts;
using Domain.Intefaces;
using MediatR;

namespace Domain.Features.Queries;

public class GetAllSalesPaginationQuery : IPagination, IRequest<PaginationResult<SaleContract>>
{
    public string? CustomerName { get; set; }
    public string? ProductName { get; set; }
    public DateTime? SaleInititalDate { get; set; }
    public DateTime? SaleEndDate { get; set; }
    public decimal? TotalSalePrice { get; set; }
    public int? Status { get; set; }

    public int? PageSize { get; set; }
    public int? Page { get; set; }
    public string? SortBy { get; set; }
    public string? OrderBy { get; set; }
}

public class GetAllSalesPaginationQueryHandler : IRequestHandler<GetAllSalesPaginationQuery, PaginationResult<SaleContract>>
{
    public GetAllSalesPaginationQueryHandler()
    {
    }

    public async Task<PaginationResult<SaleContract>> Handle(GetAllSalesPaginationQuery request, CancellationToken cancellationToken)
    {
        return new PaginationResult<SaleContract>();
    }
}

