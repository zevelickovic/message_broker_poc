using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.TestApi.DummyTest;

public class TestingEventEventHandler : DomainEventMessageHandler<TestingEvent>
{
    public TestingEventEventHandler()
    {
    }

    public override bool HandleDomainMessage(TestingEvent domainMessage)
    {
        Console.WriteLine("TestingEventEventHandler");
        if (domainMessage == null)
        {
            return false;
        }

        return true;
    }

    public override async Task<bool> HandleDomainMessageAsync(TestingEvent domainMessage)
    {
        Console.WriteLine("TestingEventEventHandler Async");
        return await Task<bool>.Run(() => true);
    }
}

public class TestingEventEventHandler2 : DomainEventMessageHandler<TestingEvent2>
{
    public TestingEventEventHandler2()
    {
    }

    public override bool HandleDomainMessage(TestingEvent2 domainMessage)
    {
        Console.WriteLine("TestingEventEventHandler");
        if (domainMessage == null)
        {
            return false;
        }

        return true;
    }

    public override async Task<bool> HandleDomainMessageAsync(TestingEvent2 domainMessage)
    {
        Console.WriteLine("TestingEventEventHandler Async");
        return await Task<bool>.Run(() => true);
    }
}