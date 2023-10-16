using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sks365.MessageBrokers.Brokers;
using Sks365.MessageBrokers.Configuration.Broker;
using Sks365.MessageBrokers.DomainMessages.Events;
using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.Extensions;

public static class IServiceProviderExtensions
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services)
    {
        return AddMessageBroker(services, null);
    }

    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Action<MessageBrokerOptions>? options)
    {
        services.AddConfiguration(options);
        services.AddSingleton<IMessageBrokerOptions, MessageBrokerOptions>(a =>
        {
            var settings = a.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;
            return settings;
        });
        services.AddSingleton<InfrastructureDomainEventsList>();
        services.AddSingleton<IDomainEventHandler, DomainEventHandler>();
        services.AddSingleton<IMessageBroker, MessageBroker>();
        return services;
    }

    private static void AddConfiguration(this IServiceCollection services, Action<MessageBrokerOptions>? options)
    {
        if (options != null)
        {
            services.Configure<MessageBrokerOptions>(options);
        }
        
        services.AddSingleton<MessageBrokerOptions>(provider =>
        {
            var monitor = provider.GetRequiredService<IOptions<MessageBrokerOptions>>()?.Value;
            if (monitor == null)
            {
                throw new ArgumentException(nameof(options));
            }
            return monitor;
        });
    }
}