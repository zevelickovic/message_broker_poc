namespace Sks365.MessageBrokers.Configuration;

public class RabbitMqMockupConfiguration
{
    public static List<RabbitMqSubscriberSettings> GetSubscriberConfigList()
    {
        return new List<RabbitMqSubscriberSettings>()
        {
            new RabbitMqSubscriberSettings()
            {
                Name = "subscriber-01",
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "test-host",
                Prefetch = 1,
                QueueName = "broker-test-queue-01",
                ExchangeName = "broker-exchange-01",
                RoutingKey = "#"
            }
        };
    }

    public static List<RabbitMqProducerSettings> GetProducerConfigList()
    {
        return new List<RabbitMqProducerSettings>()
        {
            new RabbitMqProducerSettings()
            {
                Name = "producer-01",
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "test-host",
                ExchangeName = "broker-exchange-01",
                RoutingKey = "broker-exchange-01"
            }
        };
    }
}