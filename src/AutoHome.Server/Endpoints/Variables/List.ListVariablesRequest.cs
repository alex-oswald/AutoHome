using AutoHome.Core;

namespace AutoHome.Server.Endpoints.Variables;

public class ListVariablesRequest : IPagedRequest
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
