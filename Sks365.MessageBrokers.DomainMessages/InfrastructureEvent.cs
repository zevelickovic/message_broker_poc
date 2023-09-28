using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.DomainMessages;

public class InfrastructureEvent<T> : IDomainMessage where T:  DomainEventMessage<T>
{
    public string EventType { get; set; }
    public T EventPayload { get; set; }
}