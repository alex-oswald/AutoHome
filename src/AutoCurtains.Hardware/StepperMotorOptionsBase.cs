using System.ComponentModel.DataAnnotations;

namespace AutoCurtains.Hardware;

public abstract class StepperMotorOptionsBase
{
    [Required]
    public ushort EnablePin { get; set; }

    [Required]
    public bool Simulation { get; set; } = true;

    public int StepsPerRevolution { get; set; } = 200;
}
