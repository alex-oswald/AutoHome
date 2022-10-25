using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoHome.Data;

public class EntityFrameworkTimeStampedRepository<T, TDbContext> : ITimeStampedRepository<T>
    where T : class, ITimeStamped
    where TDbContext : DbContext
{
    private readonly ILogger<EntityFrameworkTimeStampedRepository<T, TDbContext>> _logger;
    protected readonly TDbContext _dbContext;

    public EntityFrameworkTimeStampedRepository(
        ILogger<EntityFrameworkTimeStampedRepository<T, TDbContext>> logger,
        TDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<bool> DeleteOlderThanAsync(DateTime dateTime, CancellationToken cancellationToken)
    {
        var query = _dbContext.Set<T>().Where(o => o.TimeStamp < dateTime);
        var count = await query.CountAsync(cancellationToken);
        _logger.LogInformation("Deleting {count} trigger events older than {dateTime}", count, dateTime);
        _dbContext.Set<T>().RemoveRange(query);
        var deleted = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return deleted > 0;
    }
}
