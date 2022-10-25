namespace AutoHome.Server.Services;

public class TriggerLoaderHostedService : IHostedService
{
    private readonly ITriggersService _triggersService;

    public TriggerLoaderHostedService(ITriggersService triggersService)
    {
        _triggersService = triggersService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _triggersService.InitializeTriggersAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
