using AutoHome.Server.Endpoints.Triggers;

namespace AutoHome.Server.Endpoints.TriggerEvents;

public class ListTriggerEventsResult
{
    public Guid Id { get; set; }
    public Guid TriggerId { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public string Event { get; set; } = string.Empty;

    public GetTriggerResult Trigger { get; set; } = null!;
}