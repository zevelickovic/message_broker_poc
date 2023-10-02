using Newtonsoft.Json;

namespace Sks365.MessageBrokers.Configuration;

public class BrokerConfigurationBuilder
{
    public static BrokerConfigurationV2? Get()
    {
        using (StreamReader r = new StreamReader(Path.GetFullPath("message-broker-config.json")))
        {
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<BrokerConfigurationV2>(json);
        }
    }
}

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


public class RabbitMqBrokerConfiguration
{
    private readonly Dictionary<string, RabbitMqSubscriberConfiguration> _subscribersConfiguration;
    private readonly Dictionary<string, RabbitMqProducerConfiguration> _producerConfiguration;

    public RabbitMqBrokerConfiguration()
    {
        _subscribersConfiguration = new Dictionary<string, RabbitMqSubscriberConfiguration>();
        _producerConfiguration = new Dictionary<string, RabbitMqProducerConfiguration>();
        var config = BrokerConfigurationBuilder.Get();
        foreach (var item in config.RabbitMq.Subscriber)
            _subscribersConfiguration.Add(item.Name, item);
        foreach (var item in config.RabbitMq.Producers)
            _producerConfiguration.Add(item.Name, item);
    }

    public Dictionary<string, RabbitMqProducerConfiguration> GetProducersConfiguration()
    {
        return _producerConfiguration;
    }
    public Dictionary<string, RabbitMqSubscriberConfiguration> GetSubscribersConfiguration()
    {
        return _subscribersConfiguration;
    }
}