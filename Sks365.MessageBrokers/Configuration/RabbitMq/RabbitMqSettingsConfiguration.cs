using RabbitMQ.Client;

namespace Sks365.MessageBrokers.Configuration.RabbitMq;

public class RabbitMqSettingsConfiguration
{
    public string Name { get; set; }
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }
    public IConnectionFactory CreateConnectionFactory()
    {
        return new ConnectionFactory
        {
            HostName = Host,
            UserName = UserName,
            Password = Password,
            VirtualHost = VirtualHost
        };
    }
}