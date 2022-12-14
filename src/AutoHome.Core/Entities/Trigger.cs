namespace AutoHome.Core.Entities;

public class Trigger : IEntity
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Interval { get; set; }

    public Device Device { get; set; } = null!;
}
