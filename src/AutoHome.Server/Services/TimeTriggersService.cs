using AutoHome.Core;
using System.Collections.Concurrent;

namespace AutoHome.Server.Services;

public record TriggerPackage(string Name, TimeSpan Interval, ITriggerAction TriggerAction);
public record TimerPackage(Timer Timer, Device Device, TriggerPackage Package);

public interface ITriggersService
{
    bool AddTrigger(Device device, string triggerName, double intervalMilliseconds);
}

public class TriggersService : ITriggersService
{
    private readonly ConcurrentDictionary<string, TimerPackage> _triggers = new();
    private readonly ILogger<TriggersService> _logger;
    private readonly IEnumerable<ITriggerAction> _triggerActions;

    public TriggersService(
        ILogger<TriggersService> logger,
        IEnumerable<ITriggerAction> triggerActions)
    {
        _logger = logger;
        _triggerActions = triggerActions;
    }

    public bool AddTrigger(Device device, string triggerName, double intervalMilliseconds)
    {
        var action = _triggerActions.Where(o => o.Name == triggerName).First();
        var triggerPackage = new TriggerPackage(triggerName, TimeSpan.FromMilliseconds(intervalMilliseconds), action);

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

    private void Callback(object state)
    {
        _logger.LogInformation("{class}.{method} for {state}", nameof(TriggersService), nameof(Callback), state as string);

        var key = state as string;
        var success = _triggers.TryGetValue(key!, out TimerPackage? trigger);
        if (!success)
        {
            _logger.LogError("Could not get the {object} object from the dictionary with key {key}", nameof(TimerPackage), key);
        }

        _logger.LogInformation("Removing trigger for {key}", key);
        _triggers.Remove(key!, out TimerPackage _);

        _logger.LogInformation("Invoking task for {key}", key);
        Task.Run(async () => await trigger!.Package.TriggerAction.Action(trigger.Device, CancellationToken.None));

        _logger.LogInformation("Adding trigger for {key}", key);
        _triggers.TryAdd(key, trigger);

        AddTrigger(trigger.Device, trigger.Package.Name, trigger.Package.Interval.TotalMilliseconds);
    }
}
