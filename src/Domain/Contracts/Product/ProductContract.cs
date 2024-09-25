namespace Domain.Contracts;

public class ProductContract
{
    public Int64 ProductId { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; set; }
    public int Status { get; set; }
}