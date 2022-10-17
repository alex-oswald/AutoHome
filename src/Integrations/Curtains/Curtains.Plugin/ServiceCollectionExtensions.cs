using AutoHome.Core;
using AutoHome.PluginCore;
using Curtains.Plugin.TriggerActions;
using Microsoft.Extensions.DependencyInjection;

namespace Curtains.Plugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCurtainsPluginClient(this IServiceCollection services)
    {
        services.AddAutoHomePluginCore();

        services.AddScoped<ICurtainsManager, CurtainsManager>();

        services.AddTransient<ITriggerAction, OpenCurtainsTriggerAction>();
        services.AddTransient<ITriggerAction, CloseCurtainsTriggerAction>();

        return services;
    }

    public static IServiceCollection AddCurtainsPluginServer(this IServiceCollection services)
    {
        services.AddAutoHomePluginCore();

        services.AddTransient<ITriggerAction, OpenCurtainsTriggerAction>();
        services.AddTransient<ITriggerAction, CloseCurtainsTriggerAction>();

        return services;
    }
}
