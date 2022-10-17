using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AutoHome.PluginCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoHomePluginCore(this IServiceCollection services)
    {
        services.TryAddTransient<ITokenProvider, TokenProvider>();

        return services;
    }
}
