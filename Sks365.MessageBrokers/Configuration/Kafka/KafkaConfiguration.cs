namespace Sks365.MessageBrokers.Configuration.Kafka;

public class KafkaConfiguration
{
    public List<KafkaSubscriberConfiguration> Subscribers { get; set; }
    public List<KafkaProducerConfiguration> Producers { get; set; }
}