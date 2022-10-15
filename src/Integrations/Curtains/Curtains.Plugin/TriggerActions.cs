using AutoHome.PluginCore;

namespace Curtains.Plugin;

public class OpenCurtainsTriggerAction : ITriggerAction
{
    private readonly ICurtainsManager _curtainsManager;

    public OpenCurtainsTriggerAction(ICurtainsManager curtainsManager)
    {
        _curtainsManager = curtainsManager;
    }

    public string Name { get; } = "CurtainsOpen";

    public Func<Device, Task> Action
    {
        get => async (device) =>
        {
            await _curtainsManager.OpenAsync(device, CancellationToken.None);
        };
    }
}

public class CloseCurtainsTriggerActiom : ITriggerAction
{
    private readonly ICurtainsManager _curtainsManager;

    public CloseCurtainsTriggerActiom(ICurtainsManager curtainsManager)
    {
        _curtainsManager = curtainsManager;
    }

    public string Name { get; } = "CurtainsClose";

    public Func<Device, Task> Action
    {
        get => async (device) =>
        {
            await _curtainsManager.CloseAsync(device, CancellationToken.None);
        };
    }
}
