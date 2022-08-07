using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace AutoCurtains.Hardware;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoCurtainsHardware(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<A4988Options>()
            .Bind(config.GetSection(A4988Options.Section))
            .ValidateDataAnnotations();

        //services.AddSingleton<IStepperMotor, A4988>();
        services.AddSingleton<IStepperMotor, A4988>(a => new A4988(NullLogger<A4988>.Instance, new A4988Options()));

        return services;
    }
}
