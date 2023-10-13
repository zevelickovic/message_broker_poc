using Newtonsoft.Json;

namespace Sks365.MessageBrokers.Configuration.RabbitMq;

public class RabbitMqSubscriberConfiguration : RabbitMqSettingsConfiguration
{
    public string Exchange { get; set; }
    public string Queue { get; set; }
    public string RoutingKey { get; set; }
    public ushort Prefetch { get; set; } = 1;
    public bool Durable { get; set; } = true;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
}