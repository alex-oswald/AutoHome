namespace AutoHome.Data;

public interface ITimeStampedRepository<T>
    where T : class, ITimeStamped
{
    Task<bool> DeleteOlderThanAsync(DateTime dateTime, CancellationToken cancellationToken);
}
