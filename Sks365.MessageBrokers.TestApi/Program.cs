﻿// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Sks365.MessageBrokers.Configuration.Broker;
using Sks365.MessageBrokers.Configuration.RabbitMq;
using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Extensions;
using Sks365.MessageBrokers.TestApi.DummyTest;
using Sks365.MessageBrokers.TestApi.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

//builder.Services.AddMessageBroker(options =>
//{
//    options.RabbitMq = new RabbitMqConfiguration();
//    options.RabbitMq.Subscribers = new List<RabbitMqSubscriberConfiguration>();
//    options.RabbitMq.Subscribers.Add(new RabbitMqSubscriberConfiguration
//    {
//        Name = "rmq-subscriber-03",
//        Host = "localhost",
//        VirtualHost = "test-host",
//        UserName = "guest",
//        Password = "guest",
//        Exchange = "broker-exchange-01",
//        Queue = "broker-test-queue-03",
//        RoutingKey = "#",
//        Prefetch = 1,
//        Durable = true,
//        Exclusive = false,
//        AutoDelete = false
//    });
//});


IConfiguration? configurations = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
var rabbitMQChannelSettings = configurations.GetSection(typeof(MessageBrokerSettingsTest).Name);
builder.Services.Configure<MessageBrokerSettingsTest>(rabbitMQChannelSettings).AddScoped<MessageBrokerSettingsTest>();
builder.Services.AddSingleton<IConfigureOptions<MessageBrokerOptions>, ConfigurationMessageBrokerOptions>();


builder.Services.AddMessageBroker();

var brokerConfiguration = builder.Services.BuildServiceProvider().GetRequiredService<MessageBrokerOptions>();


builder.Services.AddTransient<DomainEventMessageHandler<TestingEvent>, TestingEventEventHandler>();
 builder.Services.AddTransient<DomainEventMessageHandler<TestingEvent2>, TestingEventEventHandler2>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
    app.Services.StartAllConsumers();
});

app.Lifetime.ApplicationStopped.Register(() =>
{
    app.Services.StopAllConsumers();
});

app.Run();
