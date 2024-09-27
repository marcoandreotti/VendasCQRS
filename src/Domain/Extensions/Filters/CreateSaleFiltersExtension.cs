using Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace Domain.Extensions.Filters;

public static class CreateSaleFiltersExtension
{
    public static Expression<Func<SaleEntity, bool>> FindBySaleIds(this long saleId, long companyId)
    {
        var filter = PredicateBuilder.New<SaleEntity>(true);
        filter = filter.And(item => item.SaleId == saleId);
        filter = filter.And(item => item.CompanyId == companyId);

        return filter;
    }
}
