using Sks365.MessageBrokers.Configuration;
using Sks365.MessageBrokers.Producers;

namespace Sks365.MessageBrokers.Brokers;

public class PublisherBroker : IPublisherBroker
{
    private readonly Dictionary<string, IProducer> _kafka = new();
    private readonly Dictionary<string, IProducer> _rabbitMq = new();
    public PublisherBroker()
    {
        
        var kafkaPublisherConfig = KafkaMockupConfiguration.GetProducerConfigList();
        foreach (var kv in kafkaPublisherConfig)
        {
            _kafka.Add(kv.Name, new KafkaProducer(kv.Topic));
        }
        var rabbitMqProducerConfig = RabbitMqMockupConfiguration.GetProducerConfigList();
        foreach (var config in rabbitMqProducerConfig)
        {

            _rabbitMq.Add(config.Name, new RabbitMqProducer(config));
        }
    }

    public IProducer GetKafkaPublisher(string name)
    {
        return _kafka[name];
    }
    public IProducer GetRabbiMqPublisher(string name)
    {
        return _rabbitMq[name];
    }
}