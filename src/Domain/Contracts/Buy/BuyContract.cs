using Domain.Enums;
using Domain.Extensions;

namespace Domain.Contracts;

public class BuyContractBase
{
    public Int64 BuyId { get; set; }

    public DateTime BuyDate { get; set; }
}


public class BuyContract : BuyContractBase
{
    public Int64 CustomerId { get; set; }

    public List<ProductContract> Products { get; set; }
}

public class BuyUpdateContract : BuyContractBase
{
    public Int64 CustomerId { get; set; }

    public List<ProductUpdateContract> Products { get; set; }

    public BuyStatusEnum Status { get; set; }
}

public class BuyQueryContract : BuyContractBase
{
    public CustomerContract Customer { get; set; }
    public List<ProductQueryContract> Products { get; set; }

    public decimal TotalBuyPrice { get; set; }

    public BuyStatusEnum Status { get; set; }

    public string StatusDescription => Status.GetDisplayName();
}
