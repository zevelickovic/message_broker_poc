using Sks365.MessageBrokers.DomainMessages.Handlers;
using Sks365.MessageBrokers.Subscribers;
using Sks365.MessageBrokers.Variables;
using System.Text;

namespace Sks365.MessageBrokers.Consumers;


public class Consumer : IConsumer
{
    public event ConsumerErrorHandler? ConsumerErrorHandler;

    private readonly ISubscriber _subscriber;
    private readonly IDomainEventHandler _eventMessageHandler;

    public Consumer(ISubscriber subscriber, IDomainEventHandler eventMessageHandler)
    {
        _subscriber = subscriber;
        _eventMessageHandler = eventMessageHandler;
    }

    public void Start()
    {
        _subscriber.MessageReceived += Listener_OnMessageReceived;
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

    private ConsumerResponse Listener_OnMessageReceived(string context, IDictionary<string, object> headers)
    {
        var response = new ConsumerResponse();
        if (context is not string json)
        {
            HandleMessageExeption(string.Empty, "Message payload is not string type");
            return response;
        }

        if (!headers.ContainsKey(HeaderProperties.EventType))
        {
            HandleMessageExeption(json, "Message received with Unsupported Message Type");
            return response;
        }

        var typeBytes = headers[HeaderProperties.EventType] as byte[];
        if (typeBytes == null)
        {
            HandleMessageExeption(json, "Message received with Unsupported Message Type");
            return response;
        }

        var type = Encoding.UTF8.GetString(typeBytes);
        var processMessageTask =
            Task.Run(async () => response.Success = await this._eventMessageHandler.ProcessMessageAsync(json, type)).Result;
        return response;
    }

    private void SubscriberOnError(object sender, MessageBrokerUnhandledExceptionHolder e)
    {
        ConsumerErrorHandler?.Invoke(this, e);
    }

    private void HandleMessageExeption(string payload, string message)
    {
        var unsupportedMessageType = new NotSupportedException($"{message}: {payload}");
        var ex = new MessageBrokerUnhandledExceptionHolder(unsupportedMessageType);
        ConsumerErrorHandler?.Invoke(this, ex);
    }
}