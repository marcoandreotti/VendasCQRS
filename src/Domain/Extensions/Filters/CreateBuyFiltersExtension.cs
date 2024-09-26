using Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace Domain.Extensions.Filters;

public static class CreateBuyFiltersExtension
{
    public static Expression<Func<BuyEntity, bool>> FindByBuyId(this long buyId)
    {
        var filter = PredicateBuilder.New<BuyEntity>(true);
        filter = filter.And(item => item.BuyId == buyId);

        return filter;
    }
}
