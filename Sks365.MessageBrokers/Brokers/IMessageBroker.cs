using Sks365.MessageBrokers.Consumers;
using Sks365.MessageBrokers.Producers;

namespace Sks365.MessageBrokers.Brokers;

public interface IMessageBroker
{
    IConsumer GetConsumer(string name);
    IProducer GetProducer(string name);
    void StartAllConsumers();
    void StopAllConsumers();
}