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

    /// <summary>
    /// Adds a trigger. The trigger is added to the internal dictionary. While in the dictionary, it is
    /// considered running. Once it triggers, it will recreate itself.
    /// </summary>
    Task AddTriggerAsync(Trigger trigger, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a trigger. The trigger in the dictionary is removed. The passed trigger is added to the dictionary.
    /// The trigger object is also updated in the database.
    /// </summary>
    Task UpdateTriggerAsync(Trigger trigger, CancellationToken cancellationToken);

    /// <summary>
    /// Removes a trigger. The trigger in the dictionary is removed. The triggers internal timer is set to timeout
    /// </summary>
    Task RemoveTriggerAsync(Trigger trigger, CancellationToken cancellationToken);
}

/// <summary>
/// Triggers service. Singleton.
/// </summary>
public class TriggersService : ITriggersService
{
    private readonly ConcurrentDictionary<Guid, TriggerPackage> _triggers = new();
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
        var triggersRepo = scope.ServiceProvider.GetRequiredService<IRepository<Trigger>>();
        var devicesRepo = scope.ServiceProvider.GetRequiredService<IRepository<Device>>();

        _logger.LogInformation("Initializing triggers");
        // Remove all triggers
        foreach (KeyValuePair<Guid, TriggerPackage> trigger in _triggers)
        {
            RemoveFromDictionaryAndTimeout(trigger.Value.Trigger);
        }
        _triggers.Clear();

        //  Get all triggers from the database
        var triggers = await triggersRepo.GetAllAsync(cancellationToken).ConfigureAwait(false);

        // Add each trigger from the database to the dictionary
        foreach (var trigger in triggers)
        {
            Device device = (await devicesRepo.GetAllAsync(cancellationToken,
                filter: d => d.Id == trigger.DeviceId).ConfigureAwait(false)).Single();

            AddToDictionary(trigger, device, cancellationToken);
        }
    }

    public async Task AddTriggerAsync(Trigger trigger, CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();
        var devicesRepo = scope.ServiceProvider.GetRequiredService<IRepository<Device>>();
        var triggersRepo = scope.ServiceProvider.GetRequiredService<IRepository<Trigger>>();
        var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<IRepository<TriggerEvent>>();

        // Add trigger to the db
        var addResult = await triggersRepo.AddAsync(trigger, cancellationToken).ConfigureAwait(false);

        // Add a trigger event to the db
        var triggerEvent = await triggerEventsRepo.AddAsync(new TriggerEvent
        {
            TimeStamp = DateTime.UtcNow,
            TriggerId = addResult.Id,
            Event = $"Remove trigger {trigger.Name}",
        }, cancellationToken).ConfigureAwait(false);

        // Add the trigger to the dictionary
        Device device = (await devicesRepo.GetAllAsync(cancellationToken,
            filter: d => d.Id == trigger.DeviceId).ConfigureAwait(false)).Single();
        AddToDictionary(trigger, device, cancellationToken);
    }

    public async Task UpdateTriggerAsync(Trigger trigger, CancellationToken cancellationToken)
    {
        await RemoveTriggerAsync(trigger, cancellationToken).ConfigureAwait(false);
        await AddTriggerAsync(trigger, cancellationToken).ConfigureAwait(false);
    }

    public async Task RemoveTriggerAsync(Trigger trigger, CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();
        var devicesRepo = scope.ServiceProvider.GetRequiredService<IRepository<Device>>();
        var triggersRepo = scope.ServiceProvider.GetRequiredService<IRepository<Trigger>>();
        var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<IRepository<TriggerEvent>>();

        // Remove the trigger from the dictionary
        TriggerPackage? triggerPackage = RemoveFromDictionaryAndTimeout(trigger);
        if (triggerPackage is null)
        {
            return;
        }

        // Remove the trigger from the db
        await triggersRepo.DeleteAsync(trigger, cancellationToken).ConfigureAwait(false);

        // Add a trigger event to the db
        var triggerEvent = await triggerEventsRepo.AddAsync(new TriggerEvent()
        {
            TimeStamp = DateTime.UtcNow,
            TriggerId = trigger.Id,
            Event = $"Remove trigger {triggerPackage.TriggerEventPackage.Name}",
        }, cancellationToken).ConfigureAwait(false);
    }

    private TriggerPackage? RemoveFromDictionaryAndTimeout(Trigger trigger)
    {
        // Remove the trigger from the dictionary
        var removalResult = _triggers.TryRemove(trigger.Id, out TriggerPackage? triggerPackage);
        if (!removalResult)
        {
            _logger.LogWarning("Unable to remove {id} from dictionary, it may not exist", trigger.Id);
        }
        else
        {
            triggerPackage!.Timer.Change(
                dueTime: Timeout.Infinite,
                period: Timeout.Infinite);
        }
        return triggerPackage;
    }

    private bool AddToDictionary(Trigger trigger, Device device, CancellationToken cancellationToken)
    {
        // Get the action for the trigger
        var action = _triggerActions.Where(o => o.Key == trigger.Name).FirstOrDefault();
        var triggerPackage = new TriggerEventPackage(trigger.Name, TimeSpan.FromMilliseconds(trigger.Interval), action!);

        var dateTime = DateTime.Now.Add(triggerPackage.Interval);
        _logger.LogInformation("Adding trigger {name}, will trigger at {time}",
            triggerPackage.Name, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

        var timer = new Timer(
            callback: new TimerCallback(Callback!),
            state: trigger.Id,
            dueTime: triggerPackage.Interval,
            period: Timeout.InfiniteTimeSpan);

        var addResult = _triggers.TryAdd(trigger.Id, new TriggerPackage(trigger, timer, device, triggerPackage));
        return addResult;
    }

    private async void Callback(object state)
    {
        _logger.LogInformation("{class}.{method} for {state}", nameof(TriggersService), nameof(Callback), state as string);

        Guid triggerId = (Guid)state;

        if (!_triggers.TryGetValue(triggerId!, out _))
        {
            _logger.LogInformation("{triggerId} doesn't exist. Skipping...", triggerId);
            return;
        }

        _logger.LogInformation("Removing trigger for {triggerId}", triggerId);
        if (!_triggers.TryRemove(triggerId!, out TriggerPackage? triggerPackage))
        {
            _logger.LogError("Could not get the {object} object from the dictionary with key {triggerId}", nameof(TriggerPackage), triggerId);
            throw new Exception("Could not remove object from dictionary");
        }

        using var scope = _sp.CreateScope();
        var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<IRepository<TriggerEvent>>();
        await triggerEventsRepo.AddAsync(new TriggerEvent
        {
            TriggerId = triggerPackage.Trigger.Id,
            TimeStamp = DateTime.UtcNow,
            Event = $"Invoking event {triggerPackage.TriggerEventPackage.Name}",
        }, CancellationToken.None).ConfigureAwait(false);

        _logger.LogInformation("Invoking task for {triggerId}", triggerId);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        Task.Run(async () =>
        {
            await triggerPackage!.TriggerEventPackage.TriggerAction.Action(
                triggerPackage.Device, CancellationToken.None).ConfigureAwait(false);
        })
        .ContinueWith(task =>
        {
            _logger.LogInformation("Invocation for task {triggerId} complete", triggerId);
        });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        _logger.LogInformation("Adding trigger for {triggerId}", triggerId);
        var added = _triggers.TryAdd(triggerId, triggerPackage);
        if (added)
        {
            await triggerEventsRepo.AddAsync(new TriggerEvent
            {
                TriggerId = triggerPackage.Trigger.Id,
                TimeStamp = DateTime.UtcNow,
                Event = $"Removing trigger {triggerPackage.TriggerEventPackage.Name}",
            }, CancellationToken.None).ConfigureAwait(false);
        }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        AddToDictionary(triggerPackage.Trigger, triggerPackage.Device, CancellationToken.None);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }
}
