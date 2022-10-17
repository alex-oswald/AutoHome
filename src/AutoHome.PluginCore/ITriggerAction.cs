namespace AutoHome.PluginCore;

public interface ITriggerAction
{
    string Name { get; }
    Func<Device, CancellationToken, Task> Action { get; }
}
