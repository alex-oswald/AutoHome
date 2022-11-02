namespace AutoHome.Server.Endpoints.TriggerEvents;

public class ListTriggerEventsRequest : IPagedRequest
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
