using System.ComponentModel.DataAnnotations;

namespace AutoHome;

public class CurtainConfig
{
    public const string Section = "CurtainConfig";

    [Required]
    public int Steps { get; set; }

    [Required]
    public int RPM { get; set; }
}
