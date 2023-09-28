using System.Collections.Concurrent;
using System.Reflection;

namespace Sks365.MessageBrokers.DomainMessages.Events
{
    public class InfrastructureDomainEventsList
    {
        public ConcurrentDictionary<string, Type> InfrastructureDomainEvents { get; set; } = new ConcurrentDictionary<string, Type>();

        public InfrastructureDomainEventsList()
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()
                         .Where(a => a.FullName?.StartsWith("Sks365") != null))
            {
                var infrastructureDomainEvents = a.GetTypes().Where(t =>
                    t.BaseType != null && t.BaseType.Name == typeof(DomainEventMessage<>).Name);
                foreach (var infrastructureDomainEvent in infrastructureDomainEvents)
                {
                    InfrastructureDomainEvents.TryAdd(infrastructureDomainEvent.Name, infrastructureDomainEvent);
                }
            }
        }
    }
}
