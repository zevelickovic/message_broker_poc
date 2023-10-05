namespace Sks365.MessageBrokers.Configuration.Kafka;

public class KafkaSettingsConfiguration
{
    public string Name { get; set; }
    public string Topic { get; set; }
    public Dictionary<string, string> Config { get; set; }
}