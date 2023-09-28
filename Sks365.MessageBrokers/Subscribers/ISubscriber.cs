namespace Sks365.MessageBrokers.Subscribers;

public delegate ConsumerResponse MessageHandler(string context);
public delegate void ListenerErrorHandler(object sender, SubscriberUnhandledExceptionHolder e);

public interface ISubscriber : IDisposable
{
    event MessageHandler MessageReceived;
    event ListenerErrorHandler ListenerErrorHandler;

    void Start();
    void Stop();
    void Restart();
}