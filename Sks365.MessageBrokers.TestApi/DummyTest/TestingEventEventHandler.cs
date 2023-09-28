using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.TestApi.DummyTest;

public class TestingEventEventHandler : DomainEventMessageHandler<TestingEvent>
{
    public TestingEventEventHandler()
    {
    }

    public override bool HandleDomainMessage(TestingEvent domainMessage)
    {
        Console.WriteLine("GameSessionEventReceived");
        if (domainMessage == null)
        {
            return false;
        }

        return true;
    }

    public override async Task<bool> HandleDomainMessageAsync(TestingEvent domainMessage)
    {
        Console.WriteLine("GameSessionEventReceived");
        return await Task<bool>.Run(() => true);
    }
}