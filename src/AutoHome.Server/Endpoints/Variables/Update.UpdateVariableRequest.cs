using System.ComponentModel.DataAnnotations;

namespace AutoHome.Server.Endpoints.Variables;

public class UpdateVariableRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Key { get; set; } = null!;
    [Required]
    public string Value { get; set; } = null!;
    [Required]
    public bool IsSecret { get; set; }
}
