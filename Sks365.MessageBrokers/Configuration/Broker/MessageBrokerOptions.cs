﻿using Sks365.MessageBrokers.Configuration.Kafka;
using Sks365.MessageBrokers.Configuration.RabbitMq;

namespace Sks365.MessageBrokers.Configuration.Broker;

public class MessageBrokerOptions : IMessageBrokerOptions
{
    public RabbitMqConfiguration RabbitMq { get; set; }
    public KafkaConfiguration Kafka { get; set; }
}