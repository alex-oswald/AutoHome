namespace AutoHome.Integrations.NanoCore
{
    public class IntegrationOptions : IIntegrationOptions
    {
        public string IntegrationDeviceId { get; set; }
        public string MqttBroker { get; set; }
        public string DeviceName { get; set; }
        public string MqttTopicBase { get; set; }
        public string MqttTopicStatus { get; set; }
        public int HeartbeatMilliseconds { get; set; }

        public short LedPin { get; set; }
        public short StepPin { get; set; }
        public short DirectionPin { get; set; }
        public short EnablePin { get; set; }

        public short RPM { get; set; }
        public short OpenSteps { get; set; }
        public short CloseSteps { get; set; }
    }
}
