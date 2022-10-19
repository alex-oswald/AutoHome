using AutoHome.Core;

namespace AutoHome.Client.Components;

public class ListQuery
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public static ListQuery DefaultListQuery { get; } = new()
    {
        PageIndex = DefaultPagedRequest.DefaultPageIndex,
        PageSize = DefaultPagedRequest.DefaultPageSize,
    };
}
