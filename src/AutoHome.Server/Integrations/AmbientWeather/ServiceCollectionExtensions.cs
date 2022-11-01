using Cirrus.Extensions;
using Cirrus.Models;

namespace AutoHome.Server.Integrations.AmbientWeather;

public class AmbientWeatherApiOptions
{
    public bool BackgroundServiceEnabled { get; set; }
    public string? MacAddress { get; set; }
    public List<string> ApiKeys { get; set; } = null!;
    public string ApplicationKey { get; set; } = null!;
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmbientWeatherPluginServer(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection("AmbientWeatherApi");

        services.AddOptions<AmbientWeatherApiOptions>()
            .Bind(section)
            .ValidateDataAnnotations();

        var key = section["ApiKeys:0"];
        // TODO this isn't working, why?
        //var apiKeys = section.GetValue<List<string>>(nameof(CirrusConfig.ApiKeys));
        var apiKeys = new List<string> { key };
        var applicationKey = section.GetValue<string>(nameof(CirrusConfig.ApplicationKey));
        var macAddress = section.GetValue<string?>(nameof(CirrusConfig.MacAddress));
        services
            .AddCirrusServices(cirrusConfig =>
            {
                cirrusConfig.ApiKeys = apiKeys;
                cirrusConfig.ApplicationKey = applicationKey;
                cirrusConfig.MacAddress = macAddress;
            });

        services.AddHostedService<WeatherBackupBackgroundService>();

        return services;
    }
}
