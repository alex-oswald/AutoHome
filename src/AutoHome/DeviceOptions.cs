using System.ComponentModel.DataAnnotations;

namespace AutoHome;

public class DeviceOptions
{
    public const string Section = nameof(DeviceOptions);

    [Required]
    public Guid Id { get; set; }
}
