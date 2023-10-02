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
}