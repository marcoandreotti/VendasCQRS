using Domain.Enums;
using Domain.Extensions;

namespace Domain.Contracts;

public class ProductContract
{
    public Int64 ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; set; }
}

public class ProductQueryContract : ProductContract
{
    public string Name { get; set; }

    public SaleItemStatusEnum Status { get; set; }

    public string StatusDescription => Status.GetDisplayName();

}