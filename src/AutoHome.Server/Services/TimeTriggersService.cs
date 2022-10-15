﻿using AutoHome.PluginCore;
using System.Collections.Concurrent;

namespace AutoHome.Server.Services;

public record TimeTriggerPackage(string Name, TimeSpan Time, Func<Device, Task> Task);
public record TimerPackage(Timer Timer, TimeTriggerPackage TimeTriggerPackage);

public interface ITimeTriggersService
{
    bool AddTimedTrigger(TimeTriggerPackage timeTriggerPackage);
}

public class TimeTriggersService : ITimeTriggersService
{
    private readonly ConcurrentDictionary<string, TimerPackage> _timeTriggers = new();
    private readonly ILogger<TimeTriggersService> _logger;

    public TimeTriggersService(
        ILogger<TimeTriggersService> logger)
    {
        _logger = logger;
    }

    public bool AddTimedTrigger(TimeTriggerPackage timeTriggerPackage)
    {
        var dateTime = DateTime.Now.Add(timeTriggerPackage.Time);
        _logger.LogInformation("Adding timed trigger {name}, will trigger at {time}",
            timeTriggerPackage.Name, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

        var timer = new Timer(
            callback: new TimerCallback(Callback!),
            state: timeTriggerPackage.Name,
            dueTime: timeTriggerPackage.Time,
            period: Timeout.InfiniteTimeSpan);

        return _timeTriggers.TryAdd(timeTriggerPackage.Name, new TimerPackage(timer, timeTriggerPackage));
    }

    private void Callback(object state)
    {
        _logger.LogInformation("{class}.{method} for {state}", nameof(TimeTriggersService), nameof(Callback), state as string);

        var key = state as string;
        var success = _timeTriggers.TryGetValue(key!, out TimerPackage? timeTrigger);
        if (!success)
        {
            _logger.LogError("Could not get the {object} object from the dictionary with key {key}", nameof(TimerPackage), key);
        }
        _logger.LogInformation("Removing trigger for {key}", key);
        _timeTriggers.Remove(key!, out TimerPackage _);
        _logger.LogInformation("Invoking task for {key}", key);
        Task.Run(async () => await timeTrigger!.TimeTriggerPackage.Task.Invoke().ConfigureAwait(false));
    }
}
