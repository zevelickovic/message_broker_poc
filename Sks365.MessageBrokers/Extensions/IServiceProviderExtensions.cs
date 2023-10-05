﻿using Microsoft.Extensions.Configuration;
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

    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Action<BrokerConfiguration>? options)
    {
        services.AddConfiguration(options);
        services.AddSingleton<InfrastructureDomainEventsList>();
        services.AddSingleton<IDomainEventHandler, DomainEventHandler>();

        services.AddSingleton<IMessageBroker, MessageBroker>();
        return services;
    }
    
    private static void AddConfiguration(this IServiceCollection services, Action<BrokerConfiguration>? options)
    {
        if (options != null)
        {
            services.Configure<BrokerConfiguration>(options);
            
        }
        else
        {
            IConfiguration? configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.Configure<BrokerConfiguration>(opt => configuration?.GetSection("message-broker").Bind(opt));
        }
        services.AddSingleton<BrokerConfiguration>(provider =>
        {
            var monitor = provider.GetRequiredService<IOptions<BrokerConfiguration>>()?.Value;
            if (monitor == null)
            {
                throw new ArgumentException(nameof(options));
            }
            return monitor;
        });
    }
}