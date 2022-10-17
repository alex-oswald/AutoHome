using System.ComponentModel.DataAnnotations;

namespace AutoHome.Client.Components.Triggers;

public class EditTriggerForm
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid DeviceId { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = $"{nameof(Name)} must be at least 3 characters and not more than 100.", MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public double Interval { get; set; }
}
