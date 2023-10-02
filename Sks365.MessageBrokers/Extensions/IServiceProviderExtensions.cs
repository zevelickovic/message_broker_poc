using Microsoft.Extensions.DependencyInjection;
using Sks365.MessageBrokers.Brokers;
using Sks365.MessageBrokers.DomainMessages.Events;
using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.Extensions;

public static class IServiceProviderExtensions
{
    public static IServiceCollection UseMessageBroker(this IServiceCollection services)
    {
        services.AddSingleton<InfrastructureDomainEventsList>();
        services.AddSingleton<IDomainEventHandler, DomainEventHandler>();
        services.AddSingleton<IMessageBroker, MessageBroker>();
        return services;
    }
}