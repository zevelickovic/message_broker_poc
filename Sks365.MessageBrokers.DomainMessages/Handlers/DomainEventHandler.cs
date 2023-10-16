using Newtonsoft.Json;
using Sks365.MessageBrokers.DomainMessages.Events;

namespace Sks365.MessageBrokers.DomainMessages.Handlers;

public class DomainEventHandler : IDomainEventHandler
{
    private readonly IServiceProvider _provider;
    private readonly InfrastructureDomainEventsList _infrastructureDomainEventsList;

    public DomainEventHandler(IServiceProvider provider, InfrastructureDomainEventsList infrastructureDomainEventsList)
    {
        _provider = provider;
        _infrastructureDomainEventsList = infrastructureDomainEventsList;
    }

    public async Task<bool> ProcessMessageAsync(string messageJson, string eventTypeName)
    {
        _infrastructureDomainEventsList.InfrastructureDomainEvents.TryGetValue(eventTypeName,
            out Type domaintMessageType);
        var domainMesssage = JsonConvert.DeserializeObject(Convert.ToString(messageJson),
            typeof(InfrastructureEvent<>).MakeGenericType(domaintMessageType),
            new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
        var genericType = typeof(DomainEventMessageHandler<>).MakeGenericType(domaintMessageType);
        if (_provider.GetService(genericType) is IDomainMessageHandler handler)
        {
            return await handler.HandleDomainMessageAsync(((dynamic)domainMesssage).EventPayload);
        }

        return false;
    }

    public bool ProcessMessage(string messageJson, string eventTypeName)
    {
        _infrastructureDomainEventsList.InfrastructureDomainEvents.TryGetValue(eventTypeName,
            out Type domaintMessageType);
        var domainMesssage = JsonConvert.DeserializeObject(Convert.ToString(messageJson),
            typeof(InfrastructureEvent<>).MakeGenericType(domaintMessageType),
            new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
        var genericType = typeof(DomainEventMessageHandler<>).MakeGenericType(domaintMessageType);
        if (_provider.GetService(genericType) is IDomainMessageHandler handler)
        {
            return handler.HandleDomainMessage(((dynamic)domainMesssage).EventPayload);
        }

        return false;
    }
}