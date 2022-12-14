using Microsoft.Extensions.DependencyInjection;

namespace AutoHome.PluginCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoHomePluginCore(this IServiceCollection services)
    {
        return services;
    }
}
