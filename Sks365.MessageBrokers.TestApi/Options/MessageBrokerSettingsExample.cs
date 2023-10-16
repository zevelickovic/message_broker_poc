public class MessageBrokerSettingsExample
{
    public RabbitMqConfigurationExample RabbitMq { get; set; }
    public KafkaConfigurationExample Kafka { get; set; }
}

public class RabbitMqConfigurationExample
{
    public List<RabbitMqSubscriberConfigurationExample> Subscribers { get; set; }
    public List<RabbitMqProducerConfigurationExample> Producers { get; set; }
}

public class KafkaConfigurationExample
{
    public List<KafkaSubscriberConfigurationExample> Subscribers { get; set; }
    public List<KafkaProducerConfigurationExample> Producers { get; set; }
}
public class KafkaSubscriberConfigurationExample : KafkaSettingsConfigurationExample
{
    public string GetConsumerGroupId()
    {
        return Config["group.id"];
    }
}
public class KafkaProducerConfigurationExample : KafkaSettingsConfigurationExample
{

}
public class KafkaSettingsConfigurationExample
{
    public string Name { get; set; }
    public string Topic { get; set; }
    public Dictionary<string, string> Config { get; set; }
}

public class RabbitMqSubscriberConfigurationExample : RabbitMqSettingsConfigurationExample
{
    public List<RabbitMqSubscriberBindingConfigurationExample> Bindings { get; set; }
    public ushort Prefetch { get; set; } = 1;
    public bool Durable { get; set; } = true;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
}

public class RabbitMqSubscriberBindingConfigurationExample
{
    public string Exchange { get; set; }
    public string Queue { get; set; }
    public string RoutingKey { get; set; }
}

public class RabbitMqProducerConfigurationExample : RabbitMqSettingsConfigurationExample
{
    public string Exchange { get; set; }
}

public class RabbitMqSettingsConfigurationExample
{
    public string Name { get; set; }
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }
}