using AutoHome.Integrations.NanoCore;
using AutoHome.Integrations.NanoCore.Services;
using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;

namespace AutoHome.Integrations.Curtains.Nano
{
    public class Program
    {
        public static void Main()
        {
            var host = CreateHostBuilder().Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddHostedService(typeof(WifiService));
                services.AddHostedService(typeof(HeartbeatService));
                services.AddHostedService(typeof(WebServerService<ControlController, HealthController>));
                services.AddSingleton(typeof(IHardwareManager), typeof(HardwareManager));
                services.AddSingleton(typeof(IMessagingManager), typeof(MessagingManager));

                services.AddSingleton(typeof(IIntegrationOptions), new IntegrationOptions
                {
                    IntegrationDeviceId = "8d291f88-3b06-428c-87fb-ecd0eea44d17",
                    MqttBroker = "pi.lan",
                    DeviceName = "test",
                    MqttTopicBase = "device/test",
                    MqttTopicStatus = "device/test/status",
                    HeartbeatMilliseconds = 30000,

                    LedPin = 2,
                    StepPin = 16,
                    DirectionPin = 17,
                    EnablePin = 18,

                    RPM = 20,
                    OpenSteps = 200,
                    CloseSteps = -200,
                });
            });
    }
}
