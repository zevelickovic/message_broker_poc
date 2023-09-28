using Sks365.MessageBrokers.Configuration;
using Sks365.MessageBrokers.Consumers;
using Sks365.MessageBrokers.DomainMessages.Events;
using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Subscribers;


namespace Sks365.MessageBrokers.Brokers;

public class ConsumerBroker : IConsumerBroker
{
    private readonly Dictionary<string, IConsumerV2> _kafka = new();
    private readonly Dictionary<string, IConsumerV2> _rabbitMq = new();

    public ConsumerBroker(IDomainEventHandler eventMessageHandler) 
    {
        //infrastructureDomainEventsList.Init();
        var kafkaSubscriberConfig = KafkaMockupConfiguration.GetSubscriberConfigList();
        foreach (var config in kafkaSubscriberConfig)
        {
            var subscriber = new KafkaSubscriber(config.Topic);
            _kafka.Add(config.Name, new ConsumerV2(subscriber, eventMessageHandler));
        }

        var rabbitMqSubscriberConfig = RabbitMqMockupConfiguration.GetSubscriberConfigList();
        foreach (var config in rabbitMqSubscriberConfig)
        {
            var subscriber = new RabbitMqSubscriber(config);
            _rabbitMq.Add(config.Name, new ConsumerV2(subscriber, eventMessageHandler));
        }
    }

    public IConsumerV2 GetKafkaConsumer(string name)
    {
        return _kafka[name];
    }
    public IConsumerV2 GetRabbiMqConsumer(string name)
    {
        return _rabbitMq[name];
    }

    public void StartAll()
    {
        foreach (var consumer in _kafka.Values)
        {
            Task.Run(() =>
            {
                consumer.Start();
            });
            
        }
        foreach (var consumer in _rabbitMq.Values)
        {
            Task.Run(() =>
            {
                consumer.Start();
            });
        }
    }

    public void StopAll()
    {
        foreach (var consumer in _kafka.Values)
        {
            consumer.Stop();
        }
        foreach (var consumer in _rabbitMq.Values)
        {
            consumer.Stop();
        }
    }
}