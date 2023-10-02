namespace Sks365.MessageBrokers.Configuration.Kafka;

public class KafkaSubscriberConfiguration : KafkaSettingsConfiguration
{
    public string GetConsumerGroupId()
    {
        return Config["group.id"];
    }
}