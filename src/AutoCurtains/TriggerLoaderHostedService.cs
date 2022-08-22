using AutoCurtains.Data;
using AutoCurtains.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AutoCurtains;

public interface ICurtainController
{
    Task CloseAsync();
    Task OpenAsync();
}

public class CurtainController : ICurtainController
{
    private readonly ILogger<CurtainController> _logger;
    private readonly IStepperMotor _stepperMotor;
    private readonly CurtainConfig _options;

    public CurtainController(
        ILogger<CurtainController> logger,
        IStepperMotor stepperMotor,
        IOptions<CurtainConfig> options)
    {
        _logger = logger;
        _stepperMotor = stepperMotor;
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task OpenAsync() => await RotateAsync(true);

    public async Task CloseAsync() => await RotateAsync(false);

    private async Task RotateAsync(bool clockwise)
    {
        await Task.Run(() =>
        {
            try
            {
                _stepperMotor.SetEnabledState(true);
                _stepperMotor.RPM = _options.RPM;
                _stepperMotor.Step(clockwise ? _options.Steps : -_options.Steps);
            }
            finally
            {
                _stepperMotor.SetEnabledState(false);
            }
        });
    }
}

public class TriggerLoaderHostedService : IHostedService
{
    private readonly ILogger<TriggerLoaderHostedService> _logger;
    private readonly ITimeTriggersService _timeTriggersService;
    private readonly SqliteDbContext _dbContext;
    private readonly ICurtainController _curtainController;

    public TriggerLoaderHostedService(
        ILogger<TriggerLoaderHostedService> logger,
        ITimeTriggersService timeTriggersService,
        SqliteDbContext dbContext,
        ICurtainController curtainController)
    {
        _logger = logger;
        _timeTriggersService = timeTriggersService;
        _dbContext = dbContext;
        _curtainController = curtainController;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var triggers = await _dbContext.TimeTriggers.ToListAsync();

        foreach (var trigger in triggers)
        {
            switch (trigger.Name)
            {
                case "CurtainsOpen":
                    _timeTriggersService.AddTimedTrigger(
                        new TimeTriggerPackage("CurtainsOpen", new TimeSpan(0, 0, 10), _curtainController.OpenAsync));
                    break;
                case "CurtainsClose":
                    _timeTriggersService.AddTimedTrigger(
                        new TimeTriggerPackage("CurtainsClose", new TimeSpan(0, 0, 30), _curtainController.CloseAsync));
                    break;
                default:
                    break;
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
