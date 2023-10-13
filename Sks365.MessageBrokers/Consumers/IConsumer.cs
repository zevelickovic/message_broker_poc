namespace Sks365.MessageBrokers.Consumers;

public delegate void ConsumerErrorHandler(object sender, MessageBrokerUnhandledExceptionHolder e);
public interface IConsumer
{
    void Start();
    void Stop();
}