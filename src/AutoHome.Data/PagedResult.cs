using Microsoft.EntityFrameworkCore;

namespace AutoHome.Data;

public class PagedResult<T> : IPagedResult<T>
    where T : class
{
    public int TotalItems { get; private set; }

    public int CurrentPage { get; private set; }

    public int PageSize { get; private set; }

    public int TotalPages { get; private set; }

    public int StartPage { get; private set; }

    public int EndPage { get; private set; }

    public IReadOnlyList<T> Data { get; private set; }

    public PagedResult(List<T> items, int count, int pageIndex, int pageSize)
    {
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);
        var currentPage = pageIndex;
        var startPage = currentPage - 5;
        var endPage = currentPage + 4;
        if (startPage <= 0)
        {
            endPage -= startPage - 1;
            startPage = 1;
        }
        if (endPage > totalPages)
        {
            endPage = totalPages;
            if (endPage > 10)
            {
                startPage = endPage - 9;
            }
        }

        TotalItems = count;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = totalPages;
        StartPage = startPage;
        EndPage = endPage;

        Data = new List<T>(items);
    }

    public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync()
            .ConfigureAwait(false);

        return new PagedResult<T>(items, count, pageIndex, pageSize);
    }
}

public static class IPagedResultExtensions
{
    public static IPagedResult<TResult> MapTo<TRequest, TResult>(this IPagedResult<TRequest> pagedResult, Func<TRequest, TResult> mapper)
        where TRequest : class
        where TResult : class
    {
        var items = pagedResult.Data.Select(o => mapper(o)).ToList();
        return new PagedResult<TResult>(items, pagedResult.TotalItems, pagedResult.CurrentPage, pagedResult.PageSize);
    }
}