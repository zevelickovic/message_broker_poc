using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Sks365.MessageBrokers.Configuration;

namespace Sks365.MessageBrokers.Subscribers;

public class KafkaSubscriber : ISubscriber
{
    public event MessageHandler MessageReceived;
    public event ListenerErrorHandler ListenerErrorHandler;

    private readonly IConsumer<string, string> _consumer;
    private bool _started;
    private CancellationTokenSource cts = new CancellationTokenSource();
    private readonly string _topic;
    private readonly KafkaConfiguration _configuration;

    public KafkaSubscriber(string topic)
    {
        _configuration = new KafkaConfiguration();
        var groupId = _configuration.GetConsumerGroupId();
        if (string.IsNullOrEmpty(groupId))
        {
            groupId = "group-1";
            _configuration.SetGroupId(groupId);
        }
        _consumer = new ConsumerBuilder<string, string>(_configuration.GetConsumer().AsEnumerable()).Build();
        _topic = topic;
    }
    public void Start()
    {
        var groupId = _configuration.GetConsumerGroupId();
        _started = true;
        try
        {
            _consumer.Subscribe(_topic);
            Console.WriteLine($"ConsumerV2 {_consumer.Name} in group {groupId}. Subscribed to topic regex {_topic}");
            while (_started)
            {
                var cr = _consumer.Consume(cts.Token);

                var message = cr.Message.Value;

                Console.WriteLine($"Consumed a message {cr.Message.Key} with offset {cr.Offset}, in topic: {cr.Topic}, partition: {cr.Partition.Value}, sent at: {cr.Message.Timestamp.UnixTimestampMs}.");

                var consumerResponse = MessageReceived(message);

                Thread.Sleep(20);

                _consumer.Commit(new List<TopicPartitionOffset> { cr.TopicPartitionOffset });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception #{ex}");
        }
    }

    public void Stop()
    {
        _started = false;
        _consumer.Close();
    }

    public void Restart()
    {
        Stop();
        Start();
    }
    public void Dispose()
    {
        Stop();
    }
}