using AutoHome.Core.Entities;

namespace AutoHome.Core;

public interface ITriggerAction
{
    string Name { get; }
    string Key { get; }
    Func<Device, CancellationToken, Task> Action { get; }
}
