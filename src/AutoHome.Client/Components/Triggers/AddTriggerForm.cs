using System.ComponentModel.DataAnnotations;

namespace AutoHome.Client.Components.Triggers;

public class AddTriggerForm
{
    [Required]
    public Guid DeviceId { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = $"{nameof(Name)} must be at least 3 characters and not more than 20.", MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Interval must be 0 or greater.")]
    public double Interval { get; set; }
}
