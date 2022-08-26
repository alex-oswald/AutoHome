using AutoHome.PluginCore;
using Microsoft.Extensions.DependencyInjection;

namespace Curtains.Plugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCurtainsPlugin(this IServiceCollection services)
    {
        services.AddAutoHomePluginCore();
        services.AddScoped<ICurtainsManager, CurtainsManager>();

        return services;
    }
}
