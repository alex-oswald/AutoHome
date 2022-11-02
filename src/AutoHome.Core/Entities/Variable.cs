namespace AutoHome.Core.Entities;

public class Variable : IEntity
{
    public Guid Id { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
    public bool IsSecret { get; set; }
}
