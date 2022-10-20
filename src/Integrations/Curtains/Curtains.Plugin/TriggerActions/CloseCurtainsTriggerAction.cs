using AutoHome.Core;
using AutoHome.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Curtains.Plugin.TriggerActions;

public class CloseCurtainsTriggerAction : ITriggerAction
{
    private readonly IServiceProvider _sp;

    public CloseCurtainsTriggerAction(IServiceProvider sp)
    {
        _sp = sp;
    }

    public string Name { get; } = "Close Curtains";

    public string Key { get; } = "CloseCurtains";

    public Func<Device, CancellationToken, Task> Action
    {
        get => async (device, cancellationToken) =>
        {
            var curtainsManager = _sp.GetRequiredService<ICurtainsManager>();
            await curtainsManager.CloseAsync(device, cancellationToken);
        };
    }
}
