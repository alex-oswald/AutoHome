namespace AutoHome.Server.Endpoints.TimeTriggers;

public class ListTimeTriggersResult
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan Time { get; set; }
}