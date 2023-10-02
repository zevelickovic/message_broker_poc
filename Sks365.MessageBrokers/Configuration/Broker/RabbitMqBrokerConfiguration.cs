using Sks365.MessageBrokers.Configuration.RabbitMq;

namespace Sks365.MessageBrokers.Configuration.Broker;

public class RabbitMqBrokerConfiguration
{
    private readonly Dictionary<string, RabbitMqSubscriberConfiguration> _subscribersConfiguration;
    private readonly Dictionary<string, RabbitMqProducerConfiguration> _producerConfiguration;

    public RabbitMqBrokerConfiguration()
    {
        _subscribersConfiguration = new Dictionary<string, RabbitMqSubscriberConfiguration>();
        _producerConfiguration = new Dictionary<string, RabbitMqProducerConfiguration>();
        var config = BrokerConfiguration.Get();
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