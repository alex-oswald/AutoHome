using System.ComponentModel.DataAnnotations;

namespace AutoHome.Client.Components.Devices;

public class EditDeviceForm
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid? IntegrationDeviceId { get; set; } = null;

    [Required]
    [StringLength(100, ErrorMessage = $"{nameof(Type)} must be at least 3 characters and not more than 100.", MinimumLength = 3)]
    public string Type { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = $"{nameof(Name)} must be at least 3 characters and not more than 100.", MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = $"{nameof(Uri)} must be at least 3 characters and not more than 100.", MinimumLength = 3)]
    public string Uri { get; set; } = null!;
}
