using Newtonsoft.Json;

namespace Sks365.MessageBrokers.Configuration.RabbitMq;

public class RabbitMqConfiguration
{
    [JsonProperty("subscribers")]
    public List<RabbitMqSubscriberConfiguration> Subscriber { get; set; }
    [JsonProperty("producers")]
    public List<RabbitMqProducerConfiguration> Producers { get; set; }
}