using AutoHome.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoHome.Server.Services;

public interface ICurtainsService
{
    Task<(TimeSpan? open, TimeSpan? close)> GetTimesAsync(Guid deviceId, CancellationToken cancellationToken);
    Task SaveCloseTimeAsync(Guid deviceId, TimeSpan? time, CancellationToken cancellationToken);
    Task SaveOpenTimeAsync(Guid deviceId, TimeSpan? time, CancellationToken cancellationToken);
}

public class CurtainsService : ICurtainsService
{
    private const string CURTAINS_OPEN = "CurtainsOpen";
    private const string CURTAINS_CLOSE = "CurtainsClose";

    private readonly ILogger<CurtainsService> _logger;
    private readonly SqliteDbContext _dbContext;

    public CurtainsService(
        ILogger<CurtainsService> logger,
        SqliteDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<(TimeSpan? open, TimeSpan? close)> GetTimesAsync(Guid deviceId, CancellationToken cancellationToken)
    {
        var open = await _dbContext.TimeTriggers!
            .Where(o => o.DeviceId == deviceId)
            .Where(o => o.Name == CURTAINS_OPEN)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        var close = await _dbContext.TimeTriggers!
            .Where(o => o.DeviceId == deviceId)
            .Where(o => o.Name == CURTAINS_CLOSE)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        _logger.LogInformation("{name} open:{openTime} close:{closeTime}", nameof(GetTimesAsync), open!.Time, close!.Time);
        return (open!.Time, close!.Time);
    }

    public async Task SaveOpenTimeAsync(Guid deviceId, TimeSpan? time, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{name} open:{time}", nameof(SaveOpenTimeAsync), time);
        var open = await _dbContext.TimeTriggers!
            .Where(o => o.DeviceId == deviceId)
            .Where(o => o.Name == CURTAINS_OPEN)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        // Save the open time
        if (open is not null)
        {
            if (time is null)
            {
                // If the passed in time is null, remove the db entry
                _dbContext.Remove(open);
            }
            else
            {
                // Otherwise update the db entry
                open.Time = time.Value;
                _dbContext.Update(open);
            }
        }
        else if (time is not null)
        {
            // If there is no entry, create a new one
            open ??= new()
            {
                DeviceId = deviceId,
                Name = CURTAINS_OPEN,
                Time = time.Value,
            };
            await _dbContext.AddAsync(open, cancellationToken).ConfigureAwait(false);
        }

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task SaveCloseTimeAsync(Guid deviceId, TimeSpan? time, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{name} close:{time}", nameof(SaveCloseTimeAsync), time);
        var close = await _dbContext.TimeTriggers!
            .Where(o => o.DeviceId == deviceId)
            .Where(o => o.Name == CURTAINS_CLOSE)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        // Save the close time
        if (close is not null)
        {
            if (time is null)
            {
                // If the passed in time is null, remove the db entry
                _dbContext.Remove(close);
            }
            else
            {
                // Otherwise update the db entry
                close.Time = time.Value;
                _dbContext.Update(close);
            }
        }
        else if (time is not null)
        {
            // If there is no entry, create a new one
            close ??= new()
            {
                DeviceId = deviceId,
                Name = CURTAINS_CLOSE,
                Time = time.Value,
            };
            await _dbContext.AddAsync(close, cancellationToken).ConfigureAwait(false);
        }

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
