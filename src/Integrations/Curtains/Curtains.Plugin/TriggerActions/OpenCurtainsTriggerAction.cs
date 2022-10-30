using AutoHome.Core;
using AutoHome.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Curtains.Plugin.TriggerActions;

public class OpenCurtainsTriggerAction : ITriggerAction
{
    private readonly IServiceProvider _sp;

    public OpenCurtainsTriggerAction(IServiceProvider sp)
    {
        _sp = sp;
    }

    public string Name { get; } = "Open Curtains";

    public string Key { get; } = "CurtainsOpen";

    public Func<Device, CancellationToken, Task> Action
    {
        get => async (device, cancellationToken) =>
        {
            var curtainsManager = _sp.GetRequiredService<ICurtainsManager>();
            await curtainsManager.OpenAsync(device, cancellationToken);
        };
    }
}
