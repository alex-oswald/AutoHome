using AutoHome.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoHome.Server.Services;

public interface ICurtainsService
{
    Task<(TimeSpan? open, TimeSpan? close)> GetTimesAsync(CancellationToken cancellationToken);
    Task SaveCloseTimeAsync(TimeSpan? time, CancellationToken cancellationToken);
    Task SaveOpenTimeAsync(TimeSpan? time, CancellationToken cancellationToken);
}

public class CurtainsService : ICurtainsService
{
    private const string CURTAINS_OPEN = "CurtainsOpen";
    private const string CURTAINS_CLOSE = "CurtainsClose";

    private readonly ILogger<CurtainsService> _logger;
    private readonly SqliteDbContext _dbContext;
    //private readonly DeviceOptions _options;

    public CurtainsService(
        ILogger<CurtainsService> logger,
        SqliteDbContext dbContext)
    //IOptions<DeviceOptions> options)
    {
        _logger = logger;
        _dbContext = dbContext;
        //_options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<(TimeSpan? open, TimeSpan? close)> GetTimesAsync(CancellationToken cancellationToken)
    {
        var open = await _dbContext.TimeTriggers!
            //.Where(o => o.DeviceId == _options.Id)
            .Where(o => o.Name == CURTAINS_OPEN)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        var close = await _dbContext.TimeTriggers!
            //.Where(o => o.DeviceId == _options.Id)
            .Where(o => o.Name == CURTAINS_CLOSE)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        _logger.LogInformation("{name} open:{openTime} close:{closeTime}", nameof(GetTimesAsync), open?.Time, close?.Time);
        return (open?.Time, close?.Time);
    }

    public async Task SaveOpenTimeAsync(TimeSpan? time, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{name} open:{time}", nameof(SaveOpenTimeAsync), time);
        var open = await _dbContext.TimeTriggers!
            //.Where(o => o.DeviceId == _options.Id)
            .Where(o => o.Name == CURTAINS_OPEN)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        if (open is not null)
        {
            open.Time = time;
            _dbContext.Update(open);
        }
        else
        {
            open ??= new()
            {
                //DeviceId = _options.Id,
                Name = CURTAINS_OPEN,
                Time = time,
            };
            await _dbContext.AddAsync(open, cancellationToken).ConfigureAwait(false);
        }
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task SaveCloseTimeAsync(TimeSpan? time, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{name} close:{time}", nameof(SaveCloseTimeAsync), time);
        var close = await _dbContext.TimeTriggers!
            //.Where(o => o.DeviceId == _options.Id)
            .Where(o => o.Name == CURTAINS_CLOSE)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        if (close is not null)
        {
            close.Time = time;
            _dbContext.Update(close);
        }
        else
        {
            close ??= new()
            {
                //DeviceId = _options.Id,
                Name = CURTAINS_CLOSE,
                Time = time,
            };
            await _dbContext.AddAsync(close, cancellationToken).ConfigureAwait(false);
        }
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
