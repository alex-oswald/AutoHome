using AutoHome.Data.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace AutoHome.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClientDataManagers(this IServiceCollection services)
    {
        services.AddTransient<IDevicesManager, DevicesManager>();

        return services;
    }
}
