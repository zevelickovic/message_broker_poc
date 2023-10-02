using Newtonsoft.Json;

namespace Sks365.MessageBrokers.Configuration.RabbitMq;

public class RabbitMqProducerConfiguration : RabbitMqSettingsConfiguration
{
    [JsonProperty("exchange")]
    public string ExchangeName { get; set; }
    [JsonProperty("routing-key")]
    public string RoutingKey { get; set; }
}