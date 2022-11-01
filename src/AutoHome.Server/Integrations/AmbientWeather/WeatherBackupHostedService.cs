using AutoMapper;
using Cirrus.Wrappers;
using Microsoft.Extensions.Options;

namespace AutoHome.Server.Integrations.AmbientWeather;

public class WeatherBackupBackgroundService : BackgroundService
{
    private readonly ILogger<WeatherBackupBackgroundService> _logger;
    private readonly IServiceProvider _sp;
    private readonly IMapper _mapper;
    private readonly AmbientWeatherApiOptions _options;

    public WeatherBackupBackgroundService(
        ILogger<WeatherBackupBackgroundService> logger,
        IServiceProvider sp,
        IMapper mapper,
        IOptions<AmbientWeatherApiOptions> options)
    {
        _logger = logger;
        _sp = sp;
        _mapper = mapper;
        _options = options.Value ?? throw new NullReferenceException($"{nameof(AmbientWeatherApiOptions)} is null in ctor");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_options.BackgroundServiceEnabled)
            {
                try
                {
                    using var scope = _sp.CreateScope();
                    var weather = scope.ServiceProvider.GetRequiredService<ICirrusRestWrapper>();
                    var repo = scope.ServiceProvider.GetRequiredService<IRepository<WeatherReading>>();

                    var readings = await weather.FetchDeviceDataAsync(null, 20, stoppingToken).ConfigureAwait(false);

                    foreach (var reading in readings)
                    {
                        // Try to get the reading from the database
                        var page = await repo.GetPageAsync(new DefaultPagedRequest(), stoppingToken,
                            filter: o => o.UtcDate == reading.UtcDate).ConfigureAwait(false);
                        var entity = page.Data.SingleOrDefault();
                        // If it doesn't exist, then continue to the next reading
                        if (entity is not null)
                        {
                            continue;
                        }

                        _logger.LogInformation("Adding weather reading for timestamp {time}", reading.UtcDate);
                        var weatherReading = _mapper.Map<Cirrus.Models.Device, WeatherReading>(reading);
                        await repo.AddAsync(weatherReading, stoppingToken).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting weather reading");
                }
            }

            // Wait for 30 minutes
            await Task.Delay(30 * 60 * 1000, stoppingToken);
        }
    }
}
