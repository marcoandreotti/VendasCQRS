using Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace Domain.Extensions.Filters;

public static class CreateSaleFiltersExtension
{
    public static Expression<Func<SaleEntity, bool>> FindBySaleId(this long saleId)
    {
        var filter = PredicateBuilder.New<SaleEntity>(true);
        filter = filter.And(item => item.SaleId == saleId);

        return filter;
    }
}
