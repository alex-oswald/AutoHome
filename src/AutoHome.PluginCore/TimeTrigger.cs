namespace AutoHome.PluginCore;

public class TimeTrigger
{
    public Guid DeviceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan Time { get; set; }
}
