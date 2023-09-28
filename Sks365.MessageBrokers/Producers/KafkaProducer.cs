using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sks365.MessageBrokers.Configuration;
using Sks365.MessageBrokers.DomainMessages;
using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.Producers;

public class KafkaProducer : IProducer
{
    private readonly KafkaConfiguration _configuration;
    private readonly string _topic;
    private readonly IProducer<string, string> _producer;
    public KafkaProducer(string topic)
    {
        _configuration = new KafkaConfiguration();
        _producer = new ProducerBuilder<string, string>(_configuration.GetProducer().AsEnumerable()).Build();
        _topic = topic;
    }

    private bool Publish<T>(InfrastructureEvent<T> obj, string key) where T : DomainEventMessage<T>, IDomainMessage
    {
        try
        {
            var message = JsonConvert.SerializeObject(obj);
            _producer.Produce(_topic, new Message<string, string> { Key = key, Value = message },
                (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine($"Message delivered, topic {_topic}, offset {deliveryReport.Offset}.");
                    }

                });

            Thread.Sleep(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
        return true;
    }

    public bool Publish<T>(DomainEventMessage<T> obj, string key) where T : DomainEventMessage<T>, IDomainMessage
    {
        var message = obj.CreateInfrastructureEvent();
        return Publish(message, key);
    }
}