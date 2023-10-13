using Sks365.MessageBrokers.Configuration.Kafka;
using Sks365.MessageBrokers.Configuration.RabbitMq;

namespace Sks365.MessageBrokers.Configuration.Settings;



public interface IRabbitMqConfiguration
{
    public List<IRabbitSubscriberConfigurationSettings> Subscribers { get; set; }
    public List<IRabbitProducerConfigurationSettings> Producers { get; set; }
}

public interface IRabbitMqConfigurationSettings
{
    public string Name { get; set; }
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }
}

public interface IRabbitSubscriberConfigurationSettings : IRabbitMqConfigurationSettings
{
    public string Exchange { get; set; }
    public string Queue { get; set; }
    public string RoutingKey { get; set; }
    public ushort Prefetch { get; set; }
    public bool Durable { get; set; }
    public bool Exclusive { get; set; }
    public bool AutoDelete { get; set; }
}

public interface IRabbitProducerConfigurationSettings : IRabbitMqConfigurationSettings
{
    public string Exchange { get; set; }
}

public interface IKafkaConfigurationSettings
{
    public string Name { get; set; }
    public string Topic { get; set; }
    public Dictionary<string, string> Config { get; set; }
}

public interface IKafkaSubscriberConfigurationSettings : IKafkaConfigurationSettings
{
    string GetConsumerGroupId();
}

public interface IKafkaProducerConfigurationSettings : IKafkaConfigurationSettings
{
    
}