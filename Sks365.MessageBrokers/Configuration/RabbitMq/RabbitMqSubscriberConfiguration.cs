using Newtonsoft.Json;

namespace Sks365.MessageBrokers.Configuration.RabbitMq;

public class RabbitMqSubscriberConfiguration : RabbitMqSettingsConfiguration
{
    [JsonProperty("exchange")]
    public string ExchangeName { get; set; }
    [JsonProperty("queue")]
    public string QueueName { get; set; }
    [JsonProperty("routing-key")]
    public string RoutingKey { get; set; }
    [JsonProperty("prefetch")]
    public ushort Prefetch { get; set; } = 1;
    [JsonProperty("durable")]
    public bool Durable { get; set; } = true;
    [JsonProperty("exclusive")]
    public bool Exclusive { get; set; } = false;
    [JsonProperty("auto-delete")]
    public bool AutoDelete { get; set; } = false;
}