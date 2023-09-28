namespace Sks365.MessageBrokers.DomainMessages.Handlers
{
    public abstract class DomainEventMessageHandler<TDomainMessage> : IDomainMessageHandler
        where TDomainMessage : DomainEventMessage<TDomainMessage>
    {
        public abstract bool HandleDomainMessage(TDomainMessage domainMessage);
        public abstract Task<bool> HandleDomainMessageAsync(TDomainMessage domainMessage);

        public bool HandleDomainMessage(IDomainMessage domainMessage)
        {
            return HandleDomainMessage((TDomainMessage)domainMessage);
        }

        public async Task<bool> HandleDomainMessageAsync(IDomainMessage domainMessage)
        {
            return await HandleDomainMessageAsync((TDomainMessage)domainMessage);
        }
    }
}
