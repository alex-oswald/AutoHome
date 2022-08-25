using System.ComponentModel.DataAnnotations;

namespace AutoHome.Client.Components;

public class AddDeviceForm
{
    [Required]
    public Guid? DeviceId { get; set; } = null;

    [Required]
    [StringLength(100, ErrorMessage = $"{nameof(AddDeviceForm.Type)} must be at least 3 characters and not more than 100.", MinimumLength = 3)]
    public string Type { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = $"{nameof(AddDeviceForm.Name)} must be at least 3 characters and not more than 100.", MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = $"{nameof(AddDeviceForm.Uri)} must be at least 3 characters and not more than 100.", MinimumLength = 3)]
    public string Uri { get; set; } = null!;
}
