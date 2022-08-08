using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Device.Gpio;
using System.Diagnostics;

namespace AutoCurtains.Hardware;

public class A4988Options : StepperMotorOptionsBase
{
    public const string Section = "A4988Options";

    [Required]
    public ushort StepPin { get; set; }

    [Required]
    public ushort DirectionPin { get; set; }
}

public class A4988 : StepperMotorBase
{
    private GpioController? _controller;
    private readonly ILogger<A4988> _logger;

    private readonly Stopwatch _stopwatch = new();
    private int _steps = 0;

    public A4988(ILogger<A4988> logger, IOptions<A4988Options> options)
    {
        _logger = logger;
        Options = options.Value ?? throw new ArgumentNullException(nameof(options));

        _logger.LogDebug("Initializing A4988 simulation:{simulation}", Options.Simulation);

        // If simulation mode is enabled we set the controller to null
        _controller = Options.Simulation ? null : new(PinNumberingScheme.Logical);

        _controller?.OpenPin(Options.StepPin, PinMode.Output);
        _controller?.OpenPin(Options.DirectionPin, PinMode.Output);
        _controller?.OpenPin(Options.EnablePin, PinMode.Output);
        SetEnabledState(false);
    }

    public override A4988Options Options { get; }

    public override void Dispose()
    {
        Stop();
        _controller?.Dispose();
        _controller = null;
    }

    public override void Step(int steps)
    {
        _logger.LogDebug("A4988 stepping {steps}", steps);
        double lastStepTime = 0;
        _stopwatch.Restart();
        var clockwise = steps >= 0;
        _steps = Math.Abs(steps);
        var currentStep = 0;
        // Get the delay requried between each step so the motor spins at the proper RPM
        var stepDelayMicroseconds = 60 * 1000 * 1000 / Options.StepsPerRevolution / RPM;

        // Set the direction
        _controller?.Write(Options.DirectionPin, clockwise ? PinValue.High : PinValue.Low);

        while (currentStep < _steps)
        {
            var elapsedMicroseconds = _stopwatch.Elapsed.TotalMilliseconds * 1000;

            if (elapsedMicroseconds - lastStepTime >= stepDelayMicroseconds)
            {
                lastStepTime = elapsedMicroseconds;

                _controller?.Write(Options.StepPin, PinValue.High);
                Thread.Sleep(TimeSpan.FromTicks(50)); // 5 us
                _controller?.Write(Options.StepPin, PinValue.Low);

                _logger.LogTrace("step:{currentStep} elapsed:{elapsedMicroseconds}",
                    currentStep, elapsedMicroseconds);
                currentStep++;
            }
        }

        _stopwatch.Stop();
    }

    public override void SingleStep(bool clockwise)
    {
        _logger.LogDebug("stingle step, clockwise:{clockwise}", clockwise);
        // Set the direction
        _controller?.Write(Options.DirectionPin, clockwise ? PinValue.High : PinValue.Low);

        _controller?.Write(Options.StepPin, PinValue.High);
        Thread.Sleep(TimeSpan.FromTicks(50)); // 5 us
        _controller?.Write(Options.StepPin, PinValue.Low);
    }

    public override void Stop()
    {
        _steps = 0;
        _stopwatch.Stop();
        _controller?.Write(Options.StepPin, PinValue.Low);
        _controller?.Write(Options.DirectionPin, PinValue.Low);
    }

    public override bool GetEnabledState()
    {
        _logger.LogDebug("Getting enabled state");
        if (Options.Simulation)
        {
            return _simulationEnabledState;
        }
        else
        {
            return (bool)_controller?.Read(Options.EnablePin)!;
        }
    }

    public override void SetEnabledState(bool enabled)
    {
        _logger.LogDebug("Setting enabled state to {enabled}", enabled);
        _simulationEnabledState = enabled;
        _controller?.Write(Options.EnablePin, enabled ? PinValue.Low : PinValue.High);
    }
}
