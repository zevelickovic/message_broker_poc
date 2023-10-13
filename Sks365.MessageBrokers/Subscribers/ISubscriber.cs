namespace Sks365.MessageBrokers.Subscribers;

public delegate ConsumerResponse MessageHandler(string context, IDictionary<string, object> headers);
public delegate void ListenerErrorHandler(object sender, MessageBrokerUnhandledExceptionHolder e);

public interface ISubscriber : IDisposable
{
    event MessageHandler MessageReceived;
    event ListenerErrorHandler SubscriberErrorHandler;

    void Start();
    void Stop();
    void Restart();
}