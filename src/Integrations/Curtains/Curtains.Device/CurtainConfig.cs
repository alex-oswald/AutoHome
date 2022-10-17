using System.ComponentModel.DataAnnotations;

namespace Curtains.Device;

public class CurtainConfig
{
    public const string Section = nameof(CurtainConfig);

    [Required]
    public int Steps { get; set; }

    [Required]
    public int RPM { get; set; }
}
