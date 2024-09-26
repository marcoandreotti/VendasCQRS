using Domain.Enums;
using Domain.Extensions;

namespace Domain.Contracts;

public class SaleContract
{
    public Int64 SaleId { get; set; }
    public Int64 CustomerId { get; set; }
    public List<ProductContract> Products { get; set; }
    public DateTime? SaleDate { get; set; }
}


public class SaleQueryContract
{
    public Int64 SaleId { get; set; }
    public CustomerContract Customer { get; set; }
    public List<ProductQueryContract> Products { get; set; }
    public DateTime? SaleDate { get; set; }
    public decimal TotalSalePrice { get; set; }

    public SaleStatusEnum Status { get; set; }

    public string StatusDescription => Status.GetDisplayName();
}
