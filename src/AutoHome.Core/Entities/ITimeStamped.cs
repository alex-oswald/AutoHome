namespace AutoHome.Core.Entities;

public interface ITimeStamped : IEntity
{
    DateTimeOffset TimeStamp { get; set; }
}
