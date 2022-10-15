using AutoHome.Data;
using AutoHome.PluginCore;
using AutoHome.Server.Services;
using Curtains.Plugin;

namespace AutoHome.Server;

public class TriggerLoaderHostedService : IHostedService
{
    private readonly ILogger<TriggerLoaderHostedService> _logger;
    private readonly IServiceProvider _sp;
    private readonly ITimeTriggersService _timeTriggersService;
    private readonly ICurtainsManager _curtainsManager;
    private readonly IEnumerable<ITriggerAction> _triggerActions;

    public TriggerLoaderHostedService(
        ILogger<TriggerLoaderHostedService> logger,
        IServiceProvider sp,
        ITimeTriggersService timeTriggersService,
        ICurtainsManager curtainsManager,
        IEnumerable<ITriggerAction> triggerActions)
    {
        _logger = logger;
        _sp = sp;
        _timeTriggersService = timeTriggersService;
        _curtainsManager = curtainsManager;
        _triggerActions = triggerActions;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();

        var timeTriggersRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Data.Entities.TimeTrigger>>();
        var devicesRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Data.Entities.Device>>();

        var triggers = await timeTriggersRepo!.ListAsync(cancellationToken).ConfigureAwait(false);

        foreach (var trigger in triggers!)
        {
            var device = await devicesRepo.GetByIdAsync(trigger.DeviceId, cancellationToken);

            var action = _triggerActions.Where(o => o.Name == trigger.Name).First();

            _timeTriggersService.AddTimedTrigger(
                new TimeTriggerPackage(action.Name, new TimeSpan(0, 0, 10), action.Action.Invoke(device)));

            switch (trigger.Name)
            {
                case "CurtainsOpen":
                    _timeTriggersService.AddTimedTrigger(
                        new TimeTriggerPackage("CurtainsOpen", new TimeSpan(0, 0, 10), _curtainsManager.OpenAsync(device)));
                    break;
                case "CurtainsClose":
                    _timeTriggersService.AddTimedTrigger(
                        new TimeTriggerPackage("CurtainsClose", new TimeSpan(0, 0, 30), _curtainsManager.CloseAsync(device)));
                    break;
                default:
                    break;
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
