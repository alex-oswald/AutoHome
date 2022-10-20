using System.ComponentModel.DataAnnotations;

namespace AutoHome.Server.Endpoints.Devices;

public class AddDeviceRequest
{
    [Required]
    public Guid IntegrationDeviceId { get; set; }
    [Required]
    public string Type { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Uri { get; set; } = null!;
}
