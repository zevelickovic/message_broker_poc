namespace Sks365.MessageBrokers.Configuration;

public class RabbitMqProducerSettings : RabbitMqSettings
{
    public string ExchangeName { get; set; }
    public string RoutingKey { get; set; }
}