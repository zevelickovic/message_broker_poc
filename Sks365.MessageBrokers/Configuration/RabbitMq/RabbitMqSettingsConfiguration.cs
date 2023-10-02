using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Sks365.MessageBrokers.Configuration.RabbitMq;

public class RabbitMqSettingsConfiguration
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("host")]
    public string HostName { get; set; }
    [JsonProperty("username")]
    public string UserName { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
    [JsonProperty("virtual-host")]
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