using Newtonsoft.Json;

namespace Sks365.MessageBrokers.Configuration.Kafka;

public class KafkaSettingsConfiguration
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("topic")]
    public string Topic { get; set; }
    [JsonProperty("config")]
    public Dictionary<string, string> Config { get; set; }
}