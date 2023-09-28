using Sks365.MessageBrokers.DomainMessages;
using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.Producers;

public interface IProducer
{
    bool Publish<T>(DomainEventMessage<T> obj, string key) where T : DomainEventMessage<T>, IDomainMessage;
}