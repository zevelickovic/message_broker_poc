using Sks365.MessageBrokers.Producers;

namespace Sks365.MessageBrokers.Brokers;

public interface IPublisherBroker
{
    IProducer GetKafkaPublisher(string name);
    IProducer GetRabbiMqPublisher(string name);
}