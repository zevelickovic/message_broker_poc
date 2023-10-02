using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Subscribers;

namespace Sks365.MessageBrokers.Consumers;

public class Consumer : IConsumer
{
    private readonly ISubscriber _busListener;
    private readonly IDomainEventHandler _eventMessageHandler;

    public Consumer(ISubscriber busListener, IDomainEventHandler eventMessageHandler)
    {
        _busListener = busListener;
        _eventMessageHandler = eventMessageHandler;
    }

    public void Start()
    {
        _busListener.MessageReceived += Listener_OnMessageReceived; ;
        _busListener.ListenerErrorHandler += Listener_OnError;
        _busListener.Start();
    }

    public void Stop()
    {
        if (_busListener == null)
            return;
        _busListener.ListenerErrorHandler -= Listener_OnError;
        _busListener.MessageReceived -= Listener_OnMessageReceived;
        _busListener.Stop();
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

    private void Listener_OnError(object sender, SubscriberUnhandledExceptionHolder e)
    {
        throw new NotImplementedException();
    }
}