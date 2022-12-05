using nanoFramework.M2Mqtt;
using System.Diagnostics;
using System.Text;

namespace Curtains.Nano
{
    public interface IMessaging
    {
        void PublishStatus(string value);
        void Publish(string topic, string value);
    }

    public class Messaging : IMessaging
    {
        private readonly MqttClient _client;

        public Messaging()
        {
            Debug.WriteLine("Connecting to MQTT broker");
            // Setup MQTT
            _client = new MqttClient(Configuration.MqttBroker, 1883, false, null, null, MqttSslProtocols.None)
            {
                ProtocolVersion = MqttProtocolVersion.Version_5
            };
            _client.Connect(Configuration.IntegrationDeviceId, true);
            Debug.WriteLine("Connected to MQTT broker");
            _client.Publish(Configuration.MqttTopicStatus, Encoding.UTF8.GetBytes("Device is online"));
        }

        public void PublishStatus(string value)
        {
            _client.Publish(Configuration.MqttTopicStatus, Encoding.UTF8.GetBytes(value));
        }

        public void Publish(string topic, string value)
        {
            _client.Publish(Configuration.MqttTopicBase + "/" + topic, Encoding.UTF8.GetBytes(value));
        }
    }
}
