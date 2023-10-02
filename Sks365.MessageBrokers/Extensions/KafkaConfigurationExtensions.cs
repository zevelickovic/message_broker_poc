using Microsoft.Extensions.Configuration;
using Sks365.MessageBrokers.Configuration;

namespace Sks365.MessageBrokers.Extensions;

public static class KafkaConfigurationExtensions
{
    public static IConfiguration GetConfigurationCollection(this KafkaSettingsConfiguration config)
    {
        var builder = new ConfigurationBuilder().Build();
        foreach(var item in config.Config)
            builder[item.Key] = item.Value;
        return builder;
    }
}