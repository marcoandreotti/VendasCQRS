namespace Domain.Contracts;

public class CustomerContract
{
    public CustomerContract() { }
    public CustomerContract(Int64 costumerId) => CustomerId = costumerId;

    public Int64 CustomerId { get; set; }
    public string Name { get; set; }
}
