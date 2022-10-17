using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoHome.Hardware;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoHomeHardware(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<A4988Options>()
            .Bind(config.GetSection(A4988Options.Section))
            .ValidateDataAnnotations();

        services.AddSingleton<IStepperMotor, A4988>();

        return services;
    }
}
