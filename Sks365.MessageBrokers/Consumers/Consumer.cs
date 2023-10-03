using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Subscribers;

namespace Sks365.MessageBrokers.Consumers;

public class Consumer : IConsumer
{
    private readonly ISubscriber _subscriber;
    private readonly IDomainEventHandler _eventMessageHandler;

    public Consumer(ISubscriber subscriber, IDomainEventHandler eventMessageHandler)
    {
        _subscriber = subscriber;
        _eventMessageHandler = eventMessageHandler;
    }

    public void Start()
    {
        _subscriber.MessageReceived += Listener_OnMessageReceived; ;
        _subscriber.SubscriberErrorHandler += SubscriberOnError;
        _subscriber.Start();
    }

    public void Stop()
    {
        if (_subscriber == null)
            return;
        _subscriber.SubscriberErrorHandler -= SubscriberOnError;
        _subscriber.MessageReceived -= Listener_OnMessageReceived;
        _subscriber.Stop();
    }

    private ConsumerResponse Listener_OnMessageReceived(string context)
    {
        var response = new ConsumerResponse();
        if (context is not string json)
            return response;

        var processMessageTask =
            Task.Run(async () => response.Success = await this._eventMessageHandler.ProcessMessageAsync(json));
        return response;
    }

    private void SubscriberOnError(object sender, SubscriberUnhandledExceptionHolder e)
    {
        throw new NotImplementedException();
    }
}