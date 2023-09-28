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

    public async Task<bool> ProcessMessageAsync(string messageJson)
    {
        var message = JsonConvert.DeserializeObject<dynamic>(messageJson, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
        if (message?.EventType == null)
        {
            return false;
        }
        _infrastructureDomainEventsList.InfrastructureDomainEvents.TryGetValue(message.EventType.ToString(), out Type domaintMessageType); ;
        if (domaintMessageType == null)
        {
            return false;
        }
        var domainMesssage = JsonConvert.DeserializeObject(Convert.ToString(message), typeof(InfrastructureEvent<>).MakeGenericType(domaintMessageType), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
        var genericType = typeof(DomainEventMessageHandler<>).MakeGenericType(domaintMessageType);
        if (_provider.GetService(genericType) is IDomainMessageHandler handler)
        {
            return await handler.HandleDomainMessageAsync(domainMesssage.EventPayload);
        }

        return false;
    }

    public void ProcessMessage(string messageJson)
    {
        var message = JsonConvert.DeserializeObject<dynamic>(messageJson, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
        if (message?.EventType == null)
        {
            return;
        }
        Type? domaintMessageType = Type.GetType($"Sks365.MessageBroker.DomainMessages.Events.{message.EventType}");
        if (domaintMessageType == null)
        {
            return;
        }
        var domainMessage = JsonConvert.DeserializeObject(Convert.ToString(message), typeof(InfrastructureEvent<>).MakeGenericType(domaintMessageType), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
        var genericType = typeof(DomainEventMessageHandler<>).MakeGenericType(domaintMessageType);

        if (_provider.GetService(genericType) is IDomainMessageHandler handler)
        {
            try
            {
                handler.HandleDomainMessage(domainMessage.EventPayload);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}