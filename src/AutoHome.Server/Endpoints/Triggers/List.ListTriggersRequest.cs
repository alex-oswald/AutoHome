namespace AutoHome.Server.Endpoints.Triggers;

public class ListTriggersRequest : IPagedRequest
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
