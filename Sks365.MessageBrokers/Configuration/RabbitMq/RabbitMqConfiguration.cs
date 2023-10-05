namespace Sks365.MessageBrokers.Configuration.RabbitMq;


public class RabbitMqConfiguration
{
    public List<RabbitMqSubscriberConfiguration> Subscribers { get; set; }
    public List<RabbitMqProducerConfiguration> Producers { get; set; }
}