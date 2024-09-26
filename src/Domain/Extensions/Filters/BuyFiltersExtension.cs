using Domain.Entities;
using Domain.Features.Queries;
using LinqKit;
using System.Linq.Expressions;

namespace Domain.Extensions.Filters;

public static class BuyFiltersExtension
{

    public static Expression<Func<BuyEntity, bool>> FindQueryByBuyId(this long buyId)
    {
        var filter = PredicateBuilder.New<BuyEntity>(true);
        filter = filter.And(item => item.BuyId == buyId);

        return filter;
    }

    public static Expression<Func<BuyEntity, bool>> Filter(this GetAllBuyPaginationQuery query)
    {
        var filter = PredicateBuilder.New<BuyEntity>(true);

        if (query.BuyId.HasValue)
        {
            filter = filter.And(item => item.BuyId == query.BuyId);
        }

        if (!string.IsNullOrWhiteSpace(query.CustomerName))
        {
            filter = filter.And(item => item.Customer.Name.Contains(query.CustomerName));
        }

        if (!string.IsNullOrWhiteSpace(query.ProductName))
        {
            filter = filter.And(item => item.Products.Any(x => x.Name.Contains(query.ProductName)));
        }

        if (query.BuyEndDate.HasValue && query.BuyInititalDate.HasValue)
        {
            query.BuyEndDate = query.BuyEndDate.Value.Date.AddDays(1).AddMilliseconds(-1);
            filter = filter.And(item => item.BuyDate >= query.BuyInititalDate.Value.Date && item.BuyDate <= query.BuyEndDate);
        }
        else if (query.BuyInititalDate.HasValue)
        {
            var date = query.BuyInititalDate.Value.Date;
            filter = filter.And(item => item.BuyDate == date);
        }
        else if (query.BuyEndDate.HasValue)
        {
            var date = query.BuyEndDate.Value.Date;
            filter = filter.And(item => item.BuyDate == date);
        }

        if (query.Status.HasValue)
        {
            filter = filter.And(item => item.Status == query.Status);
        }

        return filter;
    }
}