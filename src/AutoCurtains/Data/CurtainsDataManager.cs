using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AutoCurtains.Data;

public interface ICurtainsDataManager
{
    Task<(TimeSpan? open, TimeSpan? close)> GetTimesAsync(CancellationToken cancellationToken);
    Task SaveCloseTimeAsync(TimeSpan? time, CancellationToken cancellationToken);
    Task SaveOpenTimeAsync(TimeSpan? time, CancellationToken cancellationToken);
}

public class CurtainsDataManager : ICurtainsDataManager
{
    private const string CURTAINS_OPEN = "CurtainsOpen";
    private const string CURTAINS_CLOSE = "CurtainsClose";

    private readonly ILogger<CurtainsDataManager> _logger;
    private readonly SqliteDbContext _dbContext;
    private readonly DeviceOptions _options;

    public CurtainsDataManager(
        ILogger<CurtainsDataManager> logger,
        SqliteDbContext dbContext,
        IOptions<DeviceOptions> options)
    {
        _logger = logger;
        _dbContext = dbContext;
        _options = options.Value ?? throw new ArgumentException(nameof(options));
    }

    public async Task<(TimeSpan? open, TimeSpan? close)> GetTimesAsync(CancellationToken cancellationToken)
    {
        var open = await _dbContext.TimeTriggers!
            .Where(o => o.DeviceId == _options.Id)
            .Where(o => o.Name == CURTAINS_OPEN)
            .SingleOrDefaultAsync();
        var close = await _dbContext.TimeTriggers!
            .Where(o => o.DeviceId == _options.Id)
            .Where(o => o.Name == CURTAINS_CLOSE)
            .SingleOrDefaultAsync();
        return (open?.Time, close?.Time);
    }

    public async Task SaveOpenTimeAsync(TimeSpan? time, CancellationToken cancellationToken)
    {
        var open = await _dbContext.TimeTriggers!
            .Where(o => o.DeviceId == _options.Id)
            .Where(o => o.Name == CURTAINS_OPEN)
            .SingleOrDefaultAsync(cancellationToken);
        if (open is not null)
        {
            open.Time = time;
            _dbContext.Update(open);
        }
        else
        {
            open ??= new()
            {
                DeviceId = _options.Id,
                Name = CURTAINS_OPEN,
                Time = time,
            };
            await _dbContext.AddAsync(open, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveCloseTimeAsync(TimeSpan? time, CancellationToken cancellationToken)
    {
        var close = await _dbContext.TimeTriggers!
            .Where(o => o.DeviceId == _options.Id)
            .Where(o => o.Name == CURTAINS_CLOSE)
            .SingleOrDefaultAsync(cancellationToken);
        if (close is not null)
        {
            close.Time = time;
            _dbContext.Update(close);
        }
        else
        {
            close ??= new()
            {
                DeviceId = _options.Id,
                Name = CURTAINS_CLOSE,
                Time = time,
            };
            await _dbContext.AddAsync(close, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
