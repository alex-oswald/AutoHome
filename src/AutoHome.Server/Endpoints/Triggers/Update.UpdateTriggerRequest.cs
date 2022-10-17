using System.ComponentModel.DataAnnotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class UpdateTriggerRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid DeviceId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public TimeSpan Time { get; set; }
}