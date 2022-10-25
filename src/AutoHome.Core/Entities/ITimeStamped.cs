namespace AutoHome.Core.Entities;

public interface ITimeStamped : IEntity
{
    DateTime TimeStamp { get; set; }
}
