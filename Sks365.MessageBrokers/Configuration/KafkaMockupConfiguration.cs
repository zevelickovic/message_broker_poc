namespace Sks365.MessageBrokers.Configuration;

public static class KafkaMockupConfiguration
{
    public static List<KafkaProcuderConfig> GetProducerConfigList()
    {
        return new List<KafkaProcuderConfig>()
        {
            new KafkaProcuderConfig()
            {
                Name = "name-1",
                Topic = "topic-01"
            }
        };
    }

    public static List<KafkaSubscriberConfig> GetSubscriberConfigList()
    {
        return new List<KafkaSubscriberConfig>()
        {
            new KafkaSubscriberConfig()
            {
                Name = "name-1",
                Topic = "topic-01"
            }
        };
    }
}

public class KafkaProcuderConfig
{
    public string Name { get; set; }
    public string Topic { get; set; }
}

public class KafkaSubscriberConfig
{
    public string Name { get; set; }
    public string Topic { get; set; }
}