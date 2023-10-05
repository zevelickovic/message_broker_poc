using Sks365.MessageBrokers.Configuration.Broker;
using Sks365.MessageBrokers.Consumers;
using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Producers;
using Sks365.MessageBrokers.Subscribers;

namespace Sks365.MessageBrokers.Brokers;

public class MessageBroker : IMessageBroker
{
    private readonly Dictionary<string, IConsumer> _consumers = new();
    private readonly Dictionary<string, IProducer> _producers = new();

    public MessageBroker(IDomainEventHandler eventMessageHandler, BrokerConfiguration configuration)
    {
        if (configuration.Kafka?.Subscribers != null)
            foreach (var config in configuration.Kafka.Subscribers)
            {
                var subscriber = new KafkaSubscriber(config);
                _consumers.Add(config.Name, new Consumer(subscriber, eventMessageHandler));
            }

        if (configuration.RabbitMq?.Subscribers != null)
            foreach (var config in configuration.RabbitMq.Subscribers)
            {
                var subscriber = new RabbitMqSubscriber(config);
                _consumers.Add(config.Name, new Consumer(subscriber, eventMessageHandler));
            }

        if (configuration.Kafka?.Producers != null)
            foreach (var config in configuration.Kafka.Producers)
                _producers.Add(config.Name, new KafkaProducer(config));

        if (configuration.RabbitMq?.Producers != null)
            foreach (var config in configuration.RabbitMq.Producers)
                _producers.Add(config.Name, new RabbitMqProducer(config));

    }

    public IConsumer GetConsumer(string name)
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