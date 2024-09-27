using Domain.Enums;
using Domain.Extensions;

namespace Domain.Contracts;

public class SaleContractBase
{
    public Int64 SaleId { get; set; }

    public DateTime SaleDate { get; set; }
}


public class SaleContract : SaleContractBase
{
    public Int64 CustomerId { get; set; }

    public List<ProductContract> Products { get; set; }
}

public class SaleUpdateContract : SaleContractBase
{
    public Int64 CustomerId { get; set; }

    public List<ProductUpdateContract> Products { get; set; }

    public SaleStatusEnum Status { get; set; }
}

public class SaleQueryContract : SaleContractBase
{
    public CustomerContract Customer { get; set; }
    public List<ProductQueryContract> Products { get; set; }

    public decimal TotalSalePrice { get; set; }

    public SaleStatusEnum Status { get; set; }

    public string StatusDescription => Status.GetDisplayName();
}
