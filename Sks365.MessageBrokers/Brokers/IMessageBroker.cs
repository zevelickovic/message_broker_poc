using Sks365.MessageBrokers.Consumers;
using Sks365.MessageBrokers.Producers;

namespace Sks365.MessageBrokers.Brokers;

public interface IMessageBroker
{
    IConsumerV2 GetConsumer(string name);
    IProducer GetProducer(string name);
    void StartAllConsumers();
    void StopAllConsumers();
}