namespace AutoHome.Server.Endpoints.Variables;

public class UpdateVariableResult
{
    public Guid Id { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
    public bool IsSecret { get; set; }
}
