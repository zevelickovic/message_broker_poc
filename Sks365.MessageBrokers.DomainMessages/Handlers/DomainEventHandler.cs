using Newtonsoft.Json;

namespace Sks365.MessageBrokers.DomainMessages.Handlers;

public class DomainEventHandler : IDomainEventHandler
{
    private readonly IServiceProvider _provider;

    public DomainEventHandler(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<bool> ProcessMessageAsync(string messageJson, string assemblyQualifiedName)
    {
        var domaintMessageType = Type.GetType(assemblyQualifiedName);
        var domainMesssage = JsonConvert.DeserializeObject(Convert.ToString(messageJson), typeof(InfrastructureEvent<>).MakeGenericType(domaintMessageType), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
        var genericType = typeof(DomainEventMessageHandler<>).MakeGenericType(domaintMessageType);
        if (_provider.GetService(genericType) is IDomainMessageHandler handler)
        {
            return await handler.HandleDomainMessageAsync(((dynamic)domainMesssage).EventPayload);
        }

        return false;
    }

    public bool ProcessMessage(string messageJson, string assemblyQualifiedName)
    {
        var domaintMessageType = Type.GetType(assemblyQualifiedName);
        var domainMesssage = JsonConvert.DeserializeObject(Convert.ToString(messageJson), typeof(InfrastructureEvent<>).MakeGenericType(domaintMessageType), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
        var genericType = typeof(DomainEventMessageHandler<>).MakeGenericType(domaintMessageType);
        if (_provider.GetService(genericType) is IDomainMessageHandler handler)
        {
            return handler.HandleDomainMessage(((dynamic)domainMesssage).EventPayload);
        }

        return false;
    }
}