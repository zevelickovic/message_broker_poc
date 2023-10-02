using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Sks365.MessageBrokers.Configuration;


public class BrokerConfigurationV2
{
    [JsonProperty("rabbit-mq")]
    public RabbitMqConfigurationV2 RabbitMq { get; set; }
    [JsonProperty("kafka")]
    public KafkaConfigurationV2 Kafka { get; set; }
}
public class KafkaConfigurationV2
{
    [JsonProperty("subscribers")]
    public List<KafkaSubscriberConfiguration> Subscribers { get; set; }
    [JsonProperty("producers")]
    public List<KafkaProducerConfiguration> Producers { get; set; }
}

public class RabbitMqConfigurationV2
{
    [JsonProperty("subscribers")]
    public List<RabbitMqSubscriberConfiguration> Subscriber { get; set; }
    [JsonProperty("producers")]
    public List<RabbitMqProducerConfiguration> Producers { get; set; }
}

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
public class KafkaSubscriberConfiguration : KafkaSettingsConfiguration
{
    public string GetConsumerGroupId()
    {
        return Config["group-id"];
    }
}
public class KafkaProducerConfiguration : KafkaSettingsConfiguration
{

}

public class KafkaSettingsConfiguration
{
    [JsonProperty("name")]
    public string Name { get; set; } 
    [JsonProperty("topic")]
    public string Topic { get; set; }
    [JsonProperty("config")]
    public IDictionary<string, string> Config { get; set; }
}
public class RabbitMqProducerConfiguration : RabbitMqSettingsConfiguration
{
    [JsonProperty("exchange")]
    public string ExchangeName { get; set; }
    [JsonProperty("routing-key")]
    public string RoutingKey { get; set; }
}

public class RabbitMqSettingsConfiguration
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("host")]
    public string HostName { get; set; }
    [JsonProperty("username")]
    public string UserName { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
    [JsonProperty("virtual-host")]
    public string VirtualHost { get; set; }
    public IConnectionFactory CreateConnectionFactory()
    {
        return new ConnectionFactory
        {
            HostName = HostName,
            UserName = UserName,
            Password = Password,
            VirtualHost = VirtualHost
        };
    }
}