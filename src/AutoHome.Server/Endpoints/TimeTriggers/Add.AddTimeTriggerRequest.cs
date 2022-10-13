using System.ComponentModel.DataAnnotations;

namespace AutoHome.Server.Endpoints.TimeTriggers;

public class AddTimeTriggerRequest
{
    [Required]
    public Guid DeviceId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public TimeSpan Time { get; set; }
}