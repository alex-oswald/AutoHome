using AutoHome.PluginCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Curtains.Plugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCurtainsPlugin(this IServiceCollection services)
    {
        services.TryAddTransient<ITokenProvider, TokenProvider>();
        services.AddScoped<ICurtainsManager, CurtainsManager>();

        return services;
    }
}
