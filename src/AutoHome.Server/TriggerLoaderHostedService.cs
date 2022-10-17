using AutoHome.Core;
using AutoHome.Data;
using AutoHome.Server.Services;

namespace AutoHome.Server;

public class TriggerLoaderHostedService : IHostedService
{
    private readonly ILogger<TriggerLoaderHostedService> _logger;
    private readonly IServiceProvider _sp;
    private readonly ITriggersService _triggersService;
    private readonly IEnumerable<ITriggerAction> _triggerActions;

    public TriggerLoaderHostedService(
        ILogger<TriggerLoaderHostedService> logger,
        IServiceProvider sp,
        ITriggersService triggersService,
        IEnumerable<ITriggerAction> triggerActions)
    {
        _logger = logger;
        _sp = sp;
        _triggersService = triggersService;
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
            var device = (await devicesRepo.ListAsync(cancellationToken,
                filter: d => d.DeviceId == trigger.DeviceId))!.SingleOrDefault();

            await _triggersService.AddTriggerAsync(device!, trigger.Name, trigger.Interval, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
