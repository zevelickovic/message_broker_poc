using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.DomainMessages;

public interface IDomainMessageHandler
{
    bool HandleDomainMessage(IDomainMessage domainMessage);
    Task<bool> HandleDomainMessageAsync(IDomainMessage domainMessage);
}