namespace AutoHome.PluginCore;

public interface ITriggerAction
{
    string Name { get; }
    Func<Device, Task> Action { get; }
}
