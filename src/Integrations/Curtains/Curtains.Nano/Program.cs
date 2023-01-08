using AutoHome.Integrations.NanoCore;
using AutoHome.Integrations.NanoCore.Services;
using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;
using System.Threading;

namespace AutoHome.Integrations.Curtains.Nano
{
    public class Program
    {
        public static IIntegrationOptions options = new IntegrationOptions
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
        };

        public static void Main()
        {
            var source = new CancellationTokenSource();
            // Create our hardware manager and wifi service
            var hardware = new HardwareManager(options);
            hardware.Init();
            hardware.BlinkSlow(source.Token);

            //var app = new WifiService(hardware);
            // Connect to wifi
            //app.StartWifi();

            // Run the host
            //var host = CreateHostBuilder(hardware).Build();
            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(HardwareManager hardwareManager) =>
            Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddHostedService(typeof(HeartbeatService));
                services.AddHostedService(typeof(WebServerService<ControlController, HealthController>));
                services.AddSingleton(typeof(IHardwareManager), hardwareManager);
                services.AddSingleton(typeof(IMessagingManager), typeof(MessagingManager));

                services.AddSingleton(typeof(IIntegrationOptions), options);
            });
    }
}
