using AutoHome.Core;
using AutoHome.Data;
using AutoHome.Server.Services;

namespace AutoHome.Server;

public class TriggerLoaderHostedService : IHostedService
{
    private readonly ILogger<TriggerLoaderHostedService> _logger;
    private readonly IServiceProvider _sp;
    private readonly ITriggersService _timeTriggersService;
    private readonly IEnumerable<ITriggerAction> _triggerActions;

    public TriggerLoaderHostedService(
        ILogger<TriggerLoaderHostedService> logger,
        IServiceProvider sp,
        ITriggersService timeTriggersService,
        IEnumerable<ITriggerAction> triggerActions)
    {
        _logger = logger;
        _sp = sp;
        _timeTriggersService = timeTriggersService;
        _triggerActions = triggerActions;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();

        var timeTriggersRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Trigger>>();
        var devicesRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Device>>();

        var triggers = await timeTriggersRepo!.ListAsync(cancellationToken).ConfigureAwait(false);

        foreach (var trigger in triggers!)
        {
            Device device = (await devicesRepo.GetByIdAsync(trigger.DeviceId, cancellationToken)) ?? throw new NullReferenceException(nameof(device));

            var action = _triggerActions.Where(o => o.Name == trigger.Name).First();

            _timeTriggersService.AddTrigger(device, new TriggerPackage(action.Name, TimeSpan.FromMilliseconds(trigger.Interval), action));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
