namespace AutoHome.Core;

public interface IPagedRequest
{
    int? PageIndex { get; set; }
    int? PageSize { get; set; }
}

public class DefaultPagedRequest : IPagedRequest
{
    public readonly static int DefaultPageIndex = 1;
    public readonly static int DefaultPageSize = 10;
    public int? PageIndex { get; set; } = DefaultPageIndex;
    public int? PageSize { get; set; } = DefaultPageSize;
}
