using AutoHome.PluginCore;
using System.Collections.Concurrent;

namespace AutoHome.Server.Services;

public record TriggerPackage(string Name, TimeSpan Time, ITriggerAction TriggerAction);
public record TimerPackage(Timer Timer, Device Device, TriggerPackage Package);

public interface ITriggersService
{
    bool AddTrigger(Device device, TriggerPackage triggerPackage);
}

public class TriggersService : ITriggersService
{
    private readonly ConcurrentDictionary<string, TimerPackage> _triggers = new();
    private readonly ILogger<TriggersService> _logger;

    public TriggersService(
        ILogger<TriggersService> logger)
    {
        _logger = logger;
    }

    public bool AddTrigger(Device device, TriggerPackage triggerPackage)
    {
        var dateTime = DateTime.Now.Add(triggerPackage.Time);
        _logger.LogInformation("Adding trigger {name}, will trigger at {time}",
            triggerPackage.Name, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

        var timer = new Timer(
            callback: new TimerCallback(Callback!),
            state: triggerPackage.Name,
            dueTime: triggerPackage.Time,
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
    }
}
