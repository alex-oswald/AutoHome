using System.ComponentModel.DataAnnotations;

namespace AutoHome.Data.EndpointObjects.Devices;

public class UpdateDeviceRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid DeviceId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Uri { get; set; } = null!;
}
