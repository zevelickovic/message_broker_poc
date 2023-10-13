namespace Sks365.MessageBrokers.DomainMessages;

public abstract record DomainEventMessage<T> where T : DomainEventMessage<T>
{
    public virtual Type GetEventType () => typeof(T);
    public virtual InfrastructureEvent<T> CreateInfrastructureEvent()
    {
        var @event = new InfrastructureEvent<T>()
        {
            EventType = typeof(T).Name,
            EventPayload = (T)this
        };
        return @event;
    }
}