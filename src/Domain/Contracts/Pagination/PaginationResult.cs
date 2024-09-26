namespace Domain.Contracts;

public class PaginationBase
{
    public long CurrentPage { get; set; }
    public long ItemsPerPage { get; set; }
    public long Total { get; set; }
    public long TotalPages { get; set; }
    public long FirstLineOnPage => (CurrentPage - 1) * ItemsPerPage + 1;
    public long LastLineOfPage => Math.Min(CurrentPage * ItemsPerPage, Total);

    public PaginationBase() { }

    public PaginationBase(int currentPage, int itemsPerPage, long total)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        Total = total;

        if (total > 0)
            TotalPages = (int)Math.Ceiling(itemsPerPage > 0 ? (double)total / itemsPerPage : 1);
    }
}

public class PaginationResult<T> : PaginationBase where T : class
{
    public List<T> Results { get; set; }

    public PaginationResult()
    {
        Results = new List<T>(0);
    }

    public PaginationResult(int currentPage, int itemsPerPage, long total, List<T> results = null) : base
        (currentPage, itemsPerPage, total)
    {
        Results = results ?? new List<T>(0);

        if (total == 0 && results.Count > 0)
        {
            Total = results.Count;
            TotalPages = (int)Math.Ceiling(itemsPerPage > 0 ? (double)total / itemsPerPage : 1);
        }
    }
}
