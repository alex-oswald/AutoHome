namespace AutoHome.Data.Entities;

public class TriggerEvent : ITimeStamped
{
    public Guid Id { get; set; }
    public Guid TriggerId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Event { get; set; } = string.Empty;

    public Trigger Trigger { get; set; } = null!;
}
