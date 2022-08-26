using System.ComponentModel.DataAnnotations;

namespace AutoHome.Hardware;

public abstract class StepperMotorOptionsBase
{
    [Required]
    public ushort EnablePin { get; set; }

    [Required]
    public bool Simulation { get; set; }

    public int StepsPerRevolution { get; set; } = 200;
}
