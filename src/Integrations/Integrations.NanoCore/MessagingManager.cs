using nanoFramework.M2Mqtt;
using System.Diagnostics;
using System.Text;

namespace AutoHome.Integrations.NanoCore
{
    public interface IMessagingManager
    {
        void Init();
        void PublishStatus(string value);
        void Publish(string topic, string value);
    }

    public class MessagingManager : IMessagingManager
    {
        private MqttClient _client = null;
        private readonly IIntegrationOptions _options;

        public MessagingManager(IIntegrationOptions options)
        {
            _options = options;
        }

        public void Init()
        {
            Debug.WriteLine("Connecting to MQTT broker");
            // Setup MQTT
            _client = new MqttClient(_options.MqttBroker, 1883, false, null, null, MqttSslProtocols.None)
            {
                ProtocolVersion = MqttProtocolVersion.Version_5
            };
            _client.Connect(_options.IntegrationDeviceId, true);
            Debug.WriteLine("Connected to MQTT broker");
            _client.Publish(_options.MqttTopicStatus, Encoding.UTF8.GetBytes("Device is online"));
        }

        public void PublishStatus(string value)
        {
            if (_client != null)
            {
                _client.Publish(_options.MqttTopicStatus, Encoding.UTF8.GetBytes(value));
            }
        }

        public void Publish(string topic, string value)
        {
            if (_client != null)
            {
                _client.Publish(_options.MqttTopicBase + "/" + topic, Encoding.UTF8.GetBytes(value));
            }
        }
    }
}
