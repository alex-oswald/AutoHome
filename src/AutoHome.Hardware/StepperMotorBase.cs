namespace AutoHome.Hardware;

public abstract class StepperMotorBase : IStepperMotor
{
    private int _rpm = 1;
    private protected bool _simulationEnabledState = false;

    public abstract StepperMotorOptionsBase Options { get; }

    public int RPM
    {
        get => _rpm;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            _rpm = value;
        }
    }

    public abstract void Dispose();

    public abstract void Step(int steps);

    public abstract void SingleStep(bool clockwise);

    public abstract void Stop();

    public abstract bool GetEnabledState();

    public abstract void SetEnabledState(bool enabled);
}
