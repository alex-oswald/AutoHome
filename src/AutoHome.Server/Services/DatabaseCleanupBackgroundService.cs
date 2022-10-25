using AutoHome.Data;
using AutoHome.Data.Entities;
using Microsoft.Extensions.Options;

namespace AutoHome.Server.Services;

public static class DatabaseCleanupExtensions
{
    public static IServiceCollection AddDatabaseCleanupService(this IServiceCollection services)
    {
        services.AddHostedService<DatabaseCleanupBackgroundService>();
        services.Configure<DatabaseCleanupOptions>(options => { });
        return services;
    }
}

public class DatabaseCleanupOptions
{
    public int DeleteTriggerEventsOlderThanHours { get; set; } = 24;
    public int IntervalHours { get; set; } = 1;
}

public class DatabaseCleanupBackgroundService : BackgroundService
{
    private readonly ILogger<DatabaseCleanupBackgroundService> _logger;
    private readonly IServiceProvider _sp;

    public DatabaseCleanupBackgroundService(
        ILogger<DatabaseCleanupBackgroundService> logger,
        IServiceProvider sp)
    {
        _logger = logger;
        _sp = sp;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _sp.CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseCleanupOptions>>().Value
                    ?? throw new NullReferenceException($"{nameof(DatabaseCleanupOptions)} is null");
                // Wait for the given interval
                await Task.Delay(TimeSpan.FromHours(options.IntervalHours), stoppingToken);
            }

            using (var scope = _sp.CreateScope())
            {
                // Get cleanup options and the trigger repo
                var options = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseCleanupOptions>>().Value;
                var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<ITimeStampedRepository<TriggerEvent>>();

                // Delete all trigger events older than the given time
                _logger.LogInformation("Cleaning up the database");
                await triggerEventsRepo.DeleteOlderThanAsync(
                    DateTime.UtcNow.Subtract(TimeSpan.FromHours(options.DeleteTriggerEventsOlderThanHours)),
                    stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
