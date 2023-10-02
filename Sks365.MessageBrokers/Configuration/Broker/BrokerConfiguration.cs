using Newtonsoft.Json;
using Sks365.MessageBrokers.Configuration.Kafka;
using Sks365.MessageBrokers.Configuration.RabbitMq;

namespace Sks365.MessageBrokers.Configuration.Broker;


public class BrokerConfiguration
{
    [JsonProperty("rabbit-mq")]
    public RabbitMqConfiguration RabbitMq { get; set; }
    [JsonProperty("kafka")]
    public KafkaConfiguration Kafka { get; set; }

    public static BrokerConfiguration? Get()
    {
        using (StreamReader r = new StreamReader(Path.GetFullPath("message-broker-config.json")))
        {
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<BrokerConfiguration>(json);
        }
    }
}