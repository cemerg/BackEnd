public class Pagination
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public Pagination(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public Pagination() { }

    public int Skip => (Page - 1) * PageSize;

    public int Take => PageSize;
}