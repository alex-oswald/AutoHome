using nanoFramework.Hosting;
using System;
using System.Diagnostics;

namespace AutoHome.Integrations.NanoCore.Services
{
    public class HeartbeatService : SchedulerService
    {
        private readonly IHardwareManager _hardwareManager;
        private readonly IMessagingManager _messagingManager;

        public HeartbeatService(
            IHardwareManager hardwareManager,
            IMessagingManager messagingManager)
            : base(TimeSpan.FromSeconds(30))
        {
            _hardwareManager = hardwareManager;
            _messagingManager = messagingManager;
        }

        protected override void ExecuteAsync()
        {
            Debug.WriteLine("Sending heartbeat");
            _hardwareManager.BlinkFast();
            _messagingManager.Publish("status", "heartbeat");
        }
    }
}
