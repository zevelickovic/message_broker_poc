namespace Sks365.MessageBrokers.DomainMessages.Handlers;

public interface IDomainEventHandler
{
    Task<bool> ProcessMessageAsync(string messageJson, string type);
    bool ProcessMessage(string messageJson, string type);
}