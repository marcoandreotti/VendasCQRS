namespace Domain.Contracts;

public class PaginationRequest
{
    public int? PageSize { get; set; }
    public int? Page { get; set; }
}
