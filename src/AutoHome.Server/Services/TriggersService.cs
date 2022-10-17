using AutoHome.Core;
using AutoHome.Data;
using AutoHome.Data.Entities;
using System.Collections.Concurrent;

namespace AutoHome.Server.Services;

public record TriggerEventPackage(string Name, TimeSpan Interval, ITriggerAction TriggerAction);
public record TriggerPackage(Trigger Trigger, Timer Timer, Device Device, TriggerEventPackage TriggerEventPackage);

public interface ITriggersService
{
    /// <summary>
    /// Initialize the services trigger dictionary from the database.
    /// </summary>
    Task InitializeTriggersAsync(CancellationToken cancellationToken);
    Task AddTriggerAsync(Trigger trigger, CancellationToken cancellationToken);
    Task UpdateTriggerAsync(Trigger trigger, CancellationToken cancellationToken);
    Task RemoveTriggerAsync(Trigger trigger, CancellationToken cancellationToken);
}

/// <summary>
/// Triggers service. Singleton.
/// </summary>
public class TriggersService : ITriggersService
{
    private readonly ConcurrentDictionary<string, TriggerPackage> _triggers = new();
    private readonly ILogger<TriggersService> _logger;
    private readonly IEnumerable<ITriggerAction> _triggerActions;
    private readonly IServiceProvider _sp;

    public TriggersService(
        ILogger<TriggersService> logger,
        IEnumerable<ITriggerAction> triggerActions,
        IServiceProvider sp)
    {
        _logger = logger;
        _triggerActions = triggerActions;
        _sp = sp;
    }

    public async Task InitializeTriggersAsync(CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();
        var triggersRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Trigger>>();
        var devicesRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Device>>();

        _triggers.Clear();
        var triggers = await triggersRepo.ListAsync(cancellationToken).ConfigureAwait(false);

        foreach (var trigger in triggers!)
        {
            Device device = (await devicesRepo.ListAsync(cancellationToken,
                filter: d => d.DeviceId == trigger.DeviceId).ConfigureAwait(false))!.Single();

            await AddToDictAsync(trigger, device, cancellationToken);
        }
    }

    public async Task AddTriggerAsync(Trigger trigger, CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();
        var devicesRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Device>>();
        var triggersRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Trigger>>();
        var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<TriggerEvent>>();

        // Add trigger to the db
        var addResult = await triggersRepo.AddAsync(trigger, cancellationToken).ConfigureAwait(false);

        // Add a trigger event to the db
        var triggerEvent = await triggerEventsRepo.AddAsync(new TriggerEvent
        {
            TimeStamp = DateTime.UtcNow,
            TriggerId = addResult.Id,
            Event = "Add new trigger",
        }, cancellationToken).ConfigureAwait(false);

        // Add the trigger to the dictionary
        Device device = (await devicesRepo.ListAsync(cancellationToken,
            filter: d => d.DeviceId == trigger.DeviceId))!.Single();
        await AddToDictAsync(trigger, device, cancellationToken);
    }

    public async Task UpdateTriggerAsync(Trigger trigger, CancellationToken cancellationToken)
    {
        await RemoveTriggerAsync(trigger, cancellationToken).ConfigureAwait(false);
        await AddTriggerAsync(trigger, cancellationToken).ConfigureAwait(false);
    }

    public async Task RemoveTriggerAsync(Trigger trigger, CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();
        var devicesRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Device>>();
        var triggersRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Trigger>>();
        var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<TriggerEvent>>();

        // Remove the trigger from the dictionary
        var removalResult = _triggers.TryRemove(trigger.Name, out TriggerPackage? triggerPackage);
        if (!removalResult)
        {
            throw new NullReferenceException(nameof(removalResult));
        }
        triggerPackage!.Timer.Change(Timeout.Infinite, Timeout.Infinite);

        // Remove the trigger from the db
        await triggersRepo.DeleteAsync(trigger, cancellationToken).ConfigureAwait(false);

        // Add a trigger event to the db
        var triggerEvent = await triggerEventsRepo.AddAsync(new TriggerEvent()
        {
            TimeStamp = DateTime.UtcNow,
            TriggerId = trigger.Id,
            Event = "Remove trigger",
        }, cancellationToken).ConfigureAwait(false);
    }

    private Task<bool> AddToDictAsync(Trigger trigger, Device device, CancellationToken cancellationToken)
    {
        // Get the action for the trigger
        var action = _triggerActions.Where(o => o.Name == trigger.Name).FirstOrDefault();
        var triggerPackage = new TriggerEventPackage(trigger.Name, TimeSpan.FromMilliseconds(trigger.Interval), action);

        var dateTime = DateTime.Now.Add(triggerPackage.Interval);
        _logger.LogInformation("Adding trigger {name}, will trigger at {time}",
            triggerPackage.Name, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

        var timer = new Timer(
            callback: new TimerCallback(Callback!),
            state: triggerPackage.Name,
            dueTime: triggerPackage.Interval,
            period: Timeout.InfiniteTimeSpan);

        var addResult = _triggers.TryAdd(triggerPackage.Name, new TriggerPackage(trigger, timer, device, triggerPackage));
        return Task.FromResult(addResult);
    }

    private async void Callback(object state)
    {
        _logger.LogInformation("{class}.{method} for {state}", nameof(TriggersService), nameof(Callback), state as string);

        var triggerName = state as string;

        if (!_triggers.TryGetValue(triggerName!, out _))
        {
            _logger.LogInformation("{triggerName} doesn't exist. Skipping...", triggerName);
            return;
        }

        _logger.LogInformation("Removing trigger for {key}", triggerName);
        if (!_triggers.TryRemove(triggerName!, out TriggerPackage? triggerPackage))
        {
            _logger.LogError("Could not get the {object} object from the dictionary with key {key}", nameof(TriggerPackage), triggerName);
            throw new Exception("Could not remove object from dictionary");
        }

        using var scope = _sp.CreateScope();
        var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<TriggerEvent>>();
        await triggerEventsRepo.AddAsync(new TriggerEvent
        {
            TriggerId = Guid.NewGuid(),
            TimeStamp = DateTime.UtcNow,
            Event = "Invoking event",
        }, CancellationToken.None);

        _logger.LogInformation("Invoking task for {key}", triggerName);
        Task.Run(async () => await triggerPackage!.TriggerEventPackage.TriggerAction.Action(triggerPackage.Device, CancellationToken.None))
            .ContinueWith((s) => { _logger.LogInformation("Invocation for task {key} complete", triggerName); });

        _logger.LogInformation("Adding trigger for {key}", triggerName);
        _triggers.TryAdd(triggerName, triggerPackage);

        AddToDictAsync(triggerPackage.Trigger, triggerPackage.Device, CancellationToken.None);
    }
}
