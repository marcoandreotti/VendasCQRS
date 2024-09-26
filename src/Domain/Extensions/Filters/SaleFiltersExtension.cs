using Domain.Entities;
using Domain.Features.Queries;
using LinqKit;
using System.Linq.Expressions;

namespace Domain.Extensions.Filters;

public static class SaleFiltersExtension
{
    public static Expression<Func<SaleEntity, bool>> Filter(this GetAllSalesPaginationQuery query)
    {
        var filter = PredicateBuilder.New<SaleEntity>(true);
        
        if (query.SaleId.HasValue)
        {
            filter = filter.And(item => item.SaleId == query.SaleId);
        }

        if (!string.IsNullOrWhiteSpace(query.CustomerName))
        {
            filter = filter.And(item => item.Customer.Name.Contains(query.CustomerName));
        }

        if (!string.IsNullOrWhiteSpace(query.ProductName))
        {
            filter = filter.And(item => item.Products.Any(x => x.Name.Contains(query.ProductName)));
        }

        if (query.SaleEndDate.HasValue && query.SaleInititalDate.HasValue)
        {
            query.SaleEndDate = query.SaleEndDate.Value.Date.AddDays(1).AddMilliseconds(-1);
            filter = filter.And(item => item.SaleDate >= query.SaleInititalDate.Value.Date && item.SaleDate <= query.SaleEndDate);
        }
        else if (query.SaleInititalDate.HasValue)
        {
            var date = query.SaleInititalDate.Value.Date;
            filter = filter.And(item => item.SaleDate == date);
        }
        else if (query.SaleEndDate.HasValue)
        {
            var date = query.SaleEndDate.Value.Date;
            filter = filter.And(item => item.SaleDate == date);
        }


        if (query.Status.HasValue)
        {
            filter = filter.And(item => item.Status == query.Status);
        }

        return filter;
    }
}