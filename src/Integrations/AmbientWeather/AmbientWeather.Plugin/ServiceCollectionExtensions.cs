using AmbientWeather.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace Curtains.Plugin;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAmbientWeatherPluginServer(this IServiceCollection services)
	{
		services.AddHostedService<WeatherBackupHostedService>();

		return services;
	}
}
