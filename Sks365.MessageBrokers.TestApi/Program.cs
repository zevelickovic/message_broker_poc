// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sks365.MessageBrokers.Brokers;
using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Extensions;
using Sks365.MessageBrokers.TestApi.DummyTest;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.UseMessageBroker();
builder.Services.AddTransient<DomainEventMessageHandler<TestingEvent>, TestingEventEventHandler>();

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
    var consumerBroker = (IConsumerBroker)app.Services.GetService(typeof(IConsumerBroker));
    consumerBroker.StartAll();
    
    
});

app.Run();
