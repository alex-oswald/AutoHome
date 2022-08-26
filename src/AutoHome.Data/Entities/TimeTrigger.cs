namespace AutoHome.Data.Entities;

public class TimeTrigger : BaseEntity
{
    public Guid DeviceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan? Time { get; set; }
}
