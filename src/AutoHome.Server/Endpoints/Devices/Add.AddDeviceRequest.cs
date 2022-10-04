using System.ComponentModel.DataAnnotations;

namespace AutoHome.Data.EndpointObjects.Devices;

public class AddDeviceRequest
{
    [Required]
    public Guid DeviceId { get; set; }
    [Required]
    public string Type { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Uri { get; set; } = null!;
}
