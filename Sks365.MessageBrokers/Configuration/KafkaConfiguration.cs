namespace Sks365.MessageBrokers.Configuration;

public class KafkaConfiguration : BrokerConfiguration
{
    public static bool IsAutoCommitEnabled = false;
    public KafkaConfiguration() : base("kafka.consumer.properties", "kafka.producer.properties")
    {
    }

    public void SetAutoCommitConfiguration(bool value)
    {
        IsAutoCommitEnabled = value;
        SetConsumer("enable.auto.commit", value.ToString().ToLower());
    }

    public void SetGroupId(string value)
    {
        SetConsumer("group.id", value);
    }
    public string GetConsumerGroupId()
    {
        return FindConsumerValue("group.id");
    }

    public void SetGroupInstanceId(string value)
    {
        SetConsumer("group.instance.id", value);
    }

    public void SetAutoOffsetReset(string value)
    {
        SetConsumer("auto.offset.reset", value);
    }
}