using System.ComponentModel.DataAnnotations;

namespace AutoHome.Server.Endpoints.Devices;

public class UpdateDeviceRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid DeviceId { get; set; }
    [Required]
    public string Type { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Uri { get; set; } = null!;
}
