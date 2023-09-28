﻿using Sks365.MessageBrokers.DomainMessages;
using Sks365.MessageBrokers.DomainMessages.Handlers;

namespace Sks365.MessageBrokers.TestApi.DummyTest;

public class TestingEvent : DomainEventMessage<TestingEvent>, IDomainMessage
{
    public int TestId { get; set; }
}