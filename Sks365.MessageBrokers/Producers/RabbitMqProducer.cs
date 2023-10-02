using Newtonsoft.Json;
using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.DomainMessages;
using System.Text;
using RabbitMQ.Client;
using Sks365.MessageBrokers.Configuration.RabbitMq;

namespace Sks365.MessageBrokers.Producers;

public class RabbitMqProducer : IProducer
{
    private readonly RabbitMqProducerConfiguration _settings;
    private readonly IConnectionFactory _connectionFactory;

    public RabbitMqProducer(RabbitMqProducerConfiguration settings)
    {
        _settings = settings;
        _connectionFactory = _settings.CreateConnectionFactory();
    }

    private bool Publish(object obj, string routingKey)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(_settings.ExchangeName, "topic", true);
                var payload = JsonConvert.SerializeObject(obj);
                var body = Encoding.UTF8.GetBytes(payload);
                channel.BasicPublish(_settings.ExchangeName, routingKey, null, body);
            }
            return true;
        }
    }

    public bool Publish<T>(DomainEventMessage<T> obj, string key) where T : DomainEventMessage<T>, IDomainMessage
    {
        var infrastructureEvent = obj.CreateInfrastructureEvent();
        return Publish(infrastructureEvent, key);
    }
}