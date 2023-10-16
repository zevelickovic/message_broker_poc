// See https://aka.ms/new-console-template for more information
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

//from direct custom options definition
//builder.Services.AddMessageBroker(options =>
//{
//    options.RabbitMq = new RabbitMqConfiguration();
//    options.RabbitMq.Subscribers = new List<RabbitMqSubscriberConfiguration>();
//    options.RabbitMq.Subscribers.Add(new RabbitMqSubscriberConfiguration
//    {
//        Name = "rmq-subscriber-02   ",
//        Host = "localhost",
//        VirtualHost = "test-host",
//        UserName = "guest",
//        Password = "guest",
//        Bindings = new List<Binding>
//        {
//            new Binding
//            {
//                Exchange = "broker-exchange-01",
//                Queue = "broker-test-queue-05",
//                RoutingKey = "05",
//            }
//        },
//        Prefetch = 1,
//        Durable = true,
//        Exclusive = false,
//        AutoDelete = false
//    });
//    options.RabbitMq.Producers = new List<RabbitMqProducerConfiguration>();
//    options.RabbitMq.Producers.Add(new RabbitMqProducerConfiguration()
//    {
//        Name = "rmq-producer-03",
//        Host = "localhost",
//        VirtualHost = "test-host",
//        UserName = "guest",
//        Password = "guest",
//        Exchange = "broker-exchange-01"
//    });
//});

//custom configuration by user

//builder.Services.Configure<MessageBrokerSettingsExample>(options => builder.Configuration.GetSection(typeof(MessageBrokerSettingsExample).Name).Bind(options));
//builder.Services.AddSingleton<IConfigureOptions<MessageBrokerOptions>, ConfigurationMessageBrokerOptions>();
//builder.Services.AddMessageBroker();

//from appConfiguration
builder.Services.AddMessageBroker(options => builder.Configuration.GetSection("MessageBrokerOptions").Bind(options));

//test
//var brokerConfiguration = builder.Services.BuildServiceProvider().GetRequiredService<IMessageBrokerOptions>();

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
