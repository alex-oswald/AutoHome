namespace AutoHome.Server.Endpoints.Triggers;

public class UpdateTriggerResult
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long Interval { get; set; }
}