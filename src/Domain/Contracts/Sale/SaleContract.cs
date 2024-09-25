namespace Domain.Contracts;

public class SaleContract
{
    public Int64 SaleId { get; set; }
    public CustomerContract Customer { get; set; }
    public List<ProductContract> Products { get; set; }
    public DateTime? SaleDate { get; set; }
}
