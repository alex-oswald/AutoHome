using System.ComponentModel.DataAnnotations;

namespace AutoHome.Client.Components.Devices;

public class AddDeviceForm
{
    [Required]
    public Guid? IntegrationDeviceId { get; set; } = Guid.Parse("8d291f88-3b06-428c-87fb-ecd0eea44d17");

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
