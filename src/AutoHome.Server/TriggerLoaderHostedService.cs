using AutoHome.Data;
using AutoHome.Data.Entities;
using AutoHome.Server.Services;

namespace AutoHome;

public interface ICurtainController
{
    Task CloseAsync();
    Task OpenAsync();
}

public class CurtainController : ICurtainController
{
    //private readonly ILogger<CurtainController> _logger;
    //private readonly IStepperMotor _stepperMotor;
    //private readonly CurtainConfig _options;

    //public CurtainController(
    //    ILogger<CurtainController> logger,
    //    IStepperMotor stepperMotor,
    //    IOptions<CurtainConfig> options)
    //{
    //    _logger = logger;
    //    _stepperMotor = stepperMotor;
    //    _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    //}

    public async Task OpenAsync() => await RotateAsync(true);

    public async Task CloseAsync() => await RotateAsync(false);

    private async Task RotateAsync(bool clockwise)
    {
        await Task.Run(() =>
        {
            //try
            //{
            //    _stepperMotor.SetEnabledState(true);
            //    _stepperMotor.RPM = _options.RPM;
            //    _stepperMotor.Step(clockwise ? _options.Steps : -_options.Steps);
            //}
            //finally
            //{
            //    _stepperMotor.SetEnabledState(false);
            //}
        });
    }
}

public class TriggerLoaderHostedService : IHostedService
{
    private readonly ILogger<TriggerLoaderHostedService> _logger;
    private readonly IServiceProvider _sp;
    private readonly ITimeTriggersService _timeTriggersService;
    private readonly ICurtainController _curtainController;

    public TriggerLoaderHostedService(
        ILogger<TriggerLoaderHostedService> logger,
        IServiceProvider sp,
        ITimeTriggersService timeTriggersService,
        ICurtainController curtainController)
    {
        _logger = logger;
        _sp = sp;
        _timeTriggersService = timeTriggersService;
        _curtainController = curtainController;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();

        var timeTriggersRepo = scope.ServiceProvider.GetService<IAsyncRepository<TimeTrigger>>();

        var triggers = await timeTriggersRepo!.ListAsync(cancellationToken);

        foreach (var trigger in triggers!)
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
