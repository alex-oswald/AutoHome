using AutoHome.Core.Entities;

namespace AutoHome.Core;

public interface ITriggerAction
{
    string Name { get; }
    Func<Device, CancellationToken, Task> Action { get; }
}
