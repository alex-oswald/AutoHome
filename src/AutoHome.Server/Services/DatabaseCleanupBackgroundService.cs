using Microsoft.Extensions.Options;

namespace AutoHome.Server.Services;

public static class DatabaseCleanupExtensions
{
    public static IServiceCollection AddDatabaseCleanupService(this IServiceCollection services)
    {
        services.AddHostedService<TriggerHistoryCleanupBackgroundService>();
        services.Configure<DatabaseCleanupOptions>(options => { });
        return services;
    }
}

public class DatabaseCleanupOptions
{
    public int DeleteTriggerEventsOlderThanHours { get; set; } = 24;
    public int IntervalHours { get; set; } = 1;
}

public class TriggerHistoryCleanupBackgroundService : BackgroundService
{
    private readonly ILogger<TriggerHistoryCleanupBackgroundService> _logger;
    private readonly IServiceProvider _sp;

    public TriggerHistoryCleanupBackgroundService(
        ILogger<TriggerHistoryCleanupBackgroundService> logger,
        IServiceProvider sp)
    {
        _logger = logger;
        _sp = sp;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            DatabaseCleanupOptions? options;
            using (var scope = _sp.CreateScope())
            {
                options = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseCleanupOptions>>().Value
                    ?? throw new NullReferenceException($"{nameof(DatabaseCleanupOptions)} is null");
            }


            // Wait for the given interval
            await Task.Delay(TimeSpan.FromHours(options.IntervalHours), stoppingToken);

            using (var scope = _sp.CreateScope())
            {
                // Get cleanup options and the trigger repo
                var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<ITimeStampedRepository<TriggerEvent>>();

                _logger.LogInformation("Cleaning up the database");

                // Delete all trigger events older than the given time
                await triggerEventsRepo.DeleteOlderThanAsync(
                    DateTime.UtcNow.Subtract(TimeSpan.FromHours(options.DeleteTriggerEventsOlderThanHours)),
                    stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
