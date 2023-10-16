using Newtonsoft.Json;
using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.DomainMessages;
using System.Text;
using RabbitMQ.Client;
using Sks365.MessageBrokers.Configuration.RabbitMq;
using Sks365.MessageBrokers.Variables;
using Sks365.MessageBrokers.DomainMessages.Events;

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

    public bool Publish<T>(DomainEventMessage<T> obj, string key) where T : DomainEventMessage<T>, IDomainMessage
    {
        var eventName = obj.GetEventType().Name;
        
        var headers = new Dictionary<string, object>
        {
            { HeaderProperties.EventName, eventName }
        };
        var infrastructureEvent = obj.CreateInfrastructureEvent();
        return Publish(infrastructureEvent, key, headers);
    }

    private bool Publish(object obj, string routingKey, IDictionary<string, object>? headers)
    {
        //todo use single connection
        using (var connection = _connectionFactory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(_settings.Exchange, "topic", true);
                var payload = JsonConvert.SerializeObject(obj);
                var body = Encoding.UTF8.GetBytes(payload);

                var basicProperties = channel.CreateBasicProperties();
                if (headers != null)
                    basicProperties.Headers = headers;

                channel.BasicPublish(_settings.Exchange, routingKey, basicProperties, body);
            }
            return true;
        }
    }
}