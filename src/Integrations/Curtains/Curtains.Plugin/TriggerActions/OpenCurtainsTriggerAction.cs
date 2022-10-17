using AutoHome.Core;
using AutoHome.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Curtains.Plugin.TriggerActions;

public class OpenCurtainsTriggerAction : ITriggerAction
{
    public const string ACTION_NAME = "CurtainsOpen";

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
