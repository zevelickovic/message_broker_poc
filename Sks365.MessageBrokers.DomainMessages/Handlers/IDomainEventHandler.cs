namespace Sks365.MessageBrokers.DomainMessages.Handlers;

public interface IDomainEventHandler
{
    Task<bool> ProcessMessageAsync(string messageJson);
    void ProcessMessage(string messageJson);
}