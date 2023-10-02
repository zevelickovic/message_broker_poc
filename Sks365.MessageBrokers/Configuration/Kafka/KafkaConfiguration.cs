using Newtonsoft.Json;

namespace Sks365.MessageBrokers.Configuration.Kafka;

public class KafkaConfiguration
{
    [JsonProperty("subscribers")]
    public List<KafkaSubscriberConfiguration> Subscribers { get; set; }
    [JsonProperty("producers")]
    public List<KafkaProducerConfiguration> Producers { get; set; }
}