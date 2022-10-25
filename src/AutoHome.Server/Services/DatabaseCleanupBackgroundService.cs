using AutoHome.Data;
using AutoHome.Data.Entities;

namespace AutoHome.Server.Services;

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
            using var scope = _sp.CreateScope();
            var triggerEventsRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<TriggerEvent>>();


        }
    }
}
