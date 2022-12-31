using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

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

public static class NavigationManagerExtensions
{
    public static bool TryGetQueryString<T>(this NavigationManager manager, string key, out T value)
    {
        var uri = manager.ToAbsoluteUri(manager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
        {
            if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
            {
                value = (T)(object)valueAsInt;
                return true;
            }

            if (typeof(T) == typeof(string))
            {
                value = (T)(object)valueFromQueryString.ToString();
                return true;
            }

            if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
            {
                value = (T)(object)valueAsDecimal;
                return true;
            }
        }

        value = default!;
        return false;
    }

    public static ListQuery GetListQuery(this NavigationManager manager)
    {
        manager.TryGetQueryString<int>("pageIndex", out int _pageIndex);
        manager.TryGetQueryString<int>("pageSize", out int _pageSize);
        var listQuery = ListQuery.DefaultListQuery;
        if (_pageIndex > 0)
        {
            listQuery.PageIndex = _pageIndex;
        }
        if (_pageSize > 0)
        {
            listQuery.PageSize = _pageSize;
        }
        return listQuery;
    }
}