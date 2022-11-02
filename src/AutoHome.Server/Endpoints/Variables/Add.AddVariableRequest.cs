using System.ComponentModel.DataAnnotations;

namespace AutoHome.Server.Endpoints.Variables;

public class AddVariableRequest
{
    [Required]
    public string Key { get; set; } = null!;
    [Required]
    public string Value { get; set; } = null!;
    [Required]
    public bool IsSecret { get; set; }
}
