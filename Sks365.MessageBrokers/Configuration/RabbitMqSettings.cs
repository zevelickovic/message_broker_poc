using RabbitMQ.Client;

namespace Sks365.MessageBrokers.Configuration;

public class RabbitMqSettings
{
    public string Name { get; set; }
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }


    public IConnectionFactory CreateConnectionFactory()
    {
        return new ConnectionFactory
        {
            HostName = HostName,
            UserName = UserName,
            Password = Password,
            VirtualHost = VirtualHost
        };
    }
}