using AutoHome.Core.Entities;
using AutoHome.PluginCore;
using Microsoft.Extensions.DependencyInjection;

namespace Curtains.Plugin;

public class OpenCurtainsTriggerAction : ITriggerAction
{
    public const string ACTION_NAME = "CurtainsClose";

    private readonly IServiceProvider _sp;

    public OpenCurtainsTriggerAction(IServiceProvider sp)
    {
        _sp = sp;
    }

    public string Name { get; } = ACTION_NAME;

    public Func<Device, CancellationToken, Task> Action
    {
        get => async (device, cancellationToken) =>
        {
            var curtainsManager = _sp.GetRequiredService<ICurtainsManager>();
            await curtainsManager.OpenAsync(device, cancellationToken);
        };
    }
}

public class CloseCurtainsTriggerAction : ITriggerAction
{
    public const string ACTION_NAME = "CurtainsClose";

    private readonly IServiceProvider _sp;

    public CloseCurtainsTriggerAction(IServiceProvider sp)
    {
        _sp = sp;
    }

    public string Name { get; } = ACTION_NAME;

    public Func<Device, CancellationToken, Task> Action
    {
        get => async (device, cancellationToken) =>
        {
            var curtainsManager = _sp.GetRequiredService<ICurtainsManager>();
            await curtainsManager.CloseAsync(device, cancellationToken);
        };
    }
}
