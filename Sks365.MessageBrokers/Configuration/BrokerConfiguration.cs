using Microsoft.Extensions.Configuration;

namespace Sks365.MessageBrokers.Configuration;

public class BrokerConfiguration : IBrokerConfiguration
{
    private readonly IConfiguration _configurationConsumer;
    private readonly IConfiguration _configurationProducer;

    public BrokerConfiguration(string consumerConfig, string producerConfig)
    {
        _configurationProducer = new ConfigurationBuilder()
            .AddIniFile(Path.GetFullPath(producerConfig))
            .Build();
        _configurationConsumer = new ConfigurationBuilder()
            .AddIniFile(Path.GetFullPath(consumerConfig))
            .Build();
    }

    public IConfiguration GetProducer()
    {
        return _configurationProducer;
    }
    public IConfiguration GetConsumer()
    {
        return _configurationConsumer;
    }

    public void SetProducer(string key, string value)
    {
        _configurationProducer[key] = value;
    }
    public void SetConsumer(string key, string value)
    {
        _configurationConsumer[key] = value;
    }

    public string FindProducerValue(string key) => _configurationProducer.GetValue<string>(key) ?? string.Empty;
    public string FindConsumerValue(string key) => _configurationConsumer.GetValue<string>(key) ?? string.Empty;
}