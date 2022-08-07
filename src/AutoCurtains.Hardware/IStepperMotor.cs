namespace AutoCurtains.Hardware
{
    public interface IStepperMotor
    {
        StepperMotorOptionsBase Options { get; }
        int RPM { get; set; }

        bool GetEnabledState();
        void SetEnabledState(bool enabled);
        void SingleStep(bool clockwise);
        void Step(int steps);
        void Stop();
    }
}