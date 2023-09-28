using Sks365.MessageBrokers.Consumers;

namespace Sks365.MessageBrokers.Brokers;

public interface IConsumerBroker
{
    IConsumerV2 GetKafkaConsumer(string name);
    IConsumerV2 GetRabbiMqConsumer(string name);
    void StartAll();
    void StopAll();
}