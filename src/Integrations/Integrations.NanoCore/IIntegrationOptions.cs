namespace AutoHome.Integrations.NanoCore
{
    public interface IIntegrationOptions
    {
        string IntegrationDeviceId { get; set; }
        string MqttBroker { get; set; }
        string DeviceName { get; set; }
        string MqttTopicBase { get; set; }
        string MqttTopicStatus { get; set; }
        int HeartbeatMilliseconds { get; set; }

        short LedPin { get; set; }
        short StepPin { get; set; }
        short DirectionPin { get; set; }
        short EnablePin { get; set; }

        short RPM { get; set; }
        short OpenSteps { get; set; }
        short CloseSteps { get; set; }
    }
}