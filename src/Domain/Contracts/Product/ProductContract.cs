using Domain.Enums;
using Domain.Extensions;

namespace Domain.Contracts;

public class ProductContract
{
    public Int64 ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}

public class ProductUpdateContract : ProductContract
{
    public string Name { get; set; }

    public BuyItemStatusEnum Status { get; set; }
}

public class ProductQueryContract : ProductContract
{
    public string Name { get; set; }

    public BuyItemStatusEnum Status { get; set; }

    public string StatusDescription => Status.GetDisplayName();

}