﻿namespace AutoHome.Server.Endpoints.TriggerEvents;

public class ListTriggerEventsResult
{
    public Guid Id { get; set; }
    public Guid TriggerId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Event { get; set; } = string.Empty;
}