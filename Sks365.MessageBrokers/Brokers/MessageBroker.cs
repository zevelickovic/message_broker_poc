using Microsoft.Extensions.Logging;
using Sks365.MessageBrokers.Configuration.Broker;
using Sks365.MessageBrokers.Consumers;
using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Producers;
using Sks365.MessageBrokers.Subscribers;

namespace Sks365.MessageBrokers.Brokers;

public class MessageBroker : IMessageBroker
{
    private readonly ILogger _logger;
    private readonly Dictionary<string, IConsumer> _consumers = new();
    private readonly Dictionary<string, IProducer> _producers = new();

    public MessageBroker(IDomainEventHandler eventMessageHandler, IMessageBrokerOptions options, ILogger logger)
    {
        _logger = logger;
        if (options.Kafka?.Subscribers != null)
            foreach (var config in options.Kafka.Subscribers)
            {
                var subscriber = new KafkaSubscriber(config);
                _consumers.Add(config.Name, new Consumer(subscriber, eventMessageHandler));
            }

        if (options.RabbitMq?.Subscribers != null)
            foreach (var config in options.RabbitMq.Subscribers)
            {
                var subscriber = new RabbitMqSubscriber(config, logger);
                _consumers.Add(config.Name, new Consumer(subscriber, eventMessageHandler));
            }

        if (options.Kafka?.Producers != null)
            foreach (var config in options.Kafka.Producers)
                _producers.Add(config.Name, new KafkaProducer(config));

        if (options.RabbitMq?.Producers != null)
            foreach (var config in options.RabbitMq.Producers)
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