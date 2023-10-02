using Sks365.MessageBrokers.Configuration;
using Sks365.MessageBrokers.Consumers;
using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Producers;
using Sks365.MessageBrokers.Subscribers;

namespace Sks365.MessageBrokers.Brokers;

public class MessageBroker : IMessageBroker
{
    private readonly Dictionary<string, IConsumerV2> _consumers = new();
    private readonly Dictionary<string, IProducer> _producers = new();

    public MessageBroker(IDomainEventHandler eventMessageHandler)
    {
        var kafkaConfig = new KafkaBrokerConfiguration();
        var rabbitMqConfig = new RabbitMqBrokerConfiguration();

        foreach (var config in kafkaConfig.GetSubscribersConfiguration())
        {
            var subscriber = new KafkaSubscriber(config.Value);
            _consumers.Add(config.Key, new ConsumerV2(subscriber, eventMessageHandler));
        }
        foreach (var config in rabbitMqConfig.GetSubscribersConfiguration())
        {
            var subscriber = new RabbitMqSubscriber(config.Value);
            _consumers.Add(config.Key, new ConsumerV2(subscriber, eventMessageHandler));
        }
        foreach (var config in kafkaConfig.GetProducersConfiguration())
        {
            _producers.Add(config.Key, new KafkaProducer(config.Value));
        }
        foreach (var config in rabbitMqConfig.GetProducersConfiguration())
        {
            _producers.Add(config.Key, new RabbitMqProducer(config.Value));
        }
    }
    public IConsumerV2 GetConsumer(string name)
    {
        return _consumers[name];
    }
    public IProducer GetProducer(string name)
    {
        return _producers[name];
    }
    public void StartAllConsumers()
    {
        foreach (var consumer in _consumers.Values)
        {
            Task.Run(() =>
            {
                consumer.Start();
            });

        }
    }

    public void StopAllConsumers()
    {
        foreach (var consumer in _consumers.Values)
        {
            consumer.Stop();
        }
    }
}