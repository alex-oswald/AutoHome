using System.ComponentModel.DataAnnotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class AddTriggerRequest
{
    [Required]
    public Guid DeviceId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public double Interval { get; set; }
}