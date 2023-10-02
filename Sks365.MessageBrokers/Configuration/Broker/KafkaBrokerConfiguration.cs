using Sks365.MessageBrokers.Configuration.Kafka;

namespace Sks365.MessageBrokers.Configuration.Broker;

public class KafkaBrokerConfiguration
{
    private readonly Dictionary<string, KafkaSubscriberConfiguration> _subscribersConfiguration;
    private readonly Dictionary<string, KafkaProducerConfiguration> _producerConfiguration;

    public KafkaBrokerConfiguration()
    {
        _subscribersConfiguration = new Dictionary<string, KafkaSubscriberConfiguration>();
        _producerConfiguration = new Dictionary<string, KafkaProducerConfiguration>();
        var config = BrokerConfigurationBuilder.Get();
        foreach (var item in config.Kafka.Subscribers)
            _subscribersConfiguration.Add(item.Name, item);
        foreach (var item in config.Kafka.Producers)
            _producerConfiguration.Add(item.Name, item);
    }

    public Dictionary<string, KafkaSubscriberConfiguration> GetSubscribersConfiguration()
    {
        return _subscribersConfiguration;
    }

    public Dictionary<string, KafkaProducerConfiguration> GetProducersConfiguration()
    {
        return _producerConfiguration;
    }
}