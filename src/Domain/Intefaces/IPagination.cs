namespace Domain.Intefaces;

public interface IPagination
{
    int? PageSize { get; set; }
    int? Page { get; set; }
    string? SortBy { get; set; }
    string? OrderBy { get; set; } // "asc" | "desc"
}
