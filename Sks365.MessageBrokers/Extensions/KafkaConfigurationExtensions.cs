using Sks365.MessageBrokers.Configuration.Kafka;

namespace Sks365.MessageBrokers.Extensions;

public static class KafkaConfigurationExtensions
{
    public static IEnumerable<KeyValuePair<string, string>> GetConfigurationCollection(this KafkaSettingsConfiguration config)
    {
        return config.Config.Select((pair) => pair);
    }
}