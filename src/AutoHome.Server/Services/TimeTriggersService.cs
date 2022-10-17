using AutoHome.Core;
using AutoHome.Data;
using AutoHome.Data.Entities;
using System.Collections.Concurrent;

namespace AutoHome.Server.Services;

public record TriggerEventPackage(string Name, TimeSpan Interval, ITriggerAction TriggerAction);
public record TimerPackage(Timer Timer, Device Device, TriggerEventPackage EventPackage);

public interface ITriggersService
{
    Task<bool> AddTriggerAsync(Device device, string triggerName, double intervalMilliseconds, CancellationToken cancellationToken);
}

public class TriggersService : ITriggersService
{
    private readonly ConcurrentDictionary<string, TimerPackage> _triggers = new();
    private readonly ILogger<TriggersService> _logger;
    private readonly IEnumerable<ITriggerAction> _triggerActions;
    private readonly IAsyncRepository<TriggerEvent> _triggerEventsRepo;

    public TriggersService(
        ILogger<TriggersService> logger,
        IEnumerable<ITriggerAction> triggerActions,
        IAsyncRepository<TriggerEvent> triggerEventsRepo)
    {
        _logger = logger;
        _triggerActions = triggerActions;
        _triggerEventsRepo = triggerEventsRepo;
    }

    public async Task<bool> AddTriggerAsync(Device device, string triggerName, double intervalMilliseconds, CancellationToken cancellationToken)
    {
        var action = _triggerActions.Where(o => o.Name == triggerName).First();
        var triggerPackage = new TriggerEventPackage(triggerName, TimeSpan.FromMilliseconds(intervalMilliseconds), action);

        var dateTime = DateTime.Now.Add(triggerPackage.Interval);
        _logger.LogInformation("Adding trigger {name}, will trigger at {time}",
            triggerPackage.Name, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

        var timer = new Timer(
            callback: new TimerCallback(Callback!),
            state: triggerPackage.Name,
            dueTime: triggerPackage.Interval,
            period: Timeout.InfiniteTimeSpan);

        return _triggers.TryAdd(triggerPackage.Name, new TimerPackage(timer, device, triggerPackage));
    }

    private async void Callback(object state)
    {
        _logger.LogInformation("{class}.{method} for {state}", nameof(TriggersService), nameof(Callback), state as string);

        var key = state as string;
        var success = _triggers.TryGetValue(key!, out TimerPackage? timerPackage);
        if (!success)
        {
            _logger.LogError("Could not get the {object} object from the dictionary with key {key}", nameof(TimerPackage), key);
        }

        _logger.LogInformation("Removing trigger for {key}", key);
        _triggers.Remove(key!, out TimerPackage _);

        await _triggerEventsRepo.AddAsync(new TriggerEvent
        {
            TriggerId = Guid.NewGuid(),
            TimeStamp = DateTime.UtcNow,
            Event = "",
        }, CancellationToken.None);

        _logger.LogInformation("Invoking task for {key}", key);
        Task.Run(async () => await timerPackage!.EventPackage.TriggerAction.Action(timerPackage.Device, CancellationToken.None))
            .ContinueWith((s) => { _logger.LogInformation("Invocation for task {key} complete", key); });

        _logger.LogInformation("Adding trigger for {key}", key);
        _triggers.TryAdd(key, timerPackage);

        AddTriggerAsync(timerPackage.Device, timerPackage.EventPackage.Name, timerPackage.EventPackage.Interval.TotalMilliseconds, CancellationToken.None);
    }
}
