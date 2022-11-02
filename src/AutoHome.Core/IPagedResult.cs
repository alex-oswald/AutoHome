namespace AutoHome.Core;

public interface IPagedResult<T>
    where T : class
{
    int TotalItems { get; }
    int CurrentPage { get; }
    int PageSize { get; }
    int TotalPages { get; }
    int StartPage { get; }
    int EndPage { get; }
    IReadOnlyList<T> Data { get; }
}
