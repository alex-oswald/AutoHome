using AutoHome.Core.Entities;

namespace AutoHome.Core;

public interface ITimeStampedRepository<T>
    where T : class, ITimeStamped
{
    Task<bool> DeleteOlderThanAsync(DateTime dateTime, CancellationToken cancellationToken);
}
