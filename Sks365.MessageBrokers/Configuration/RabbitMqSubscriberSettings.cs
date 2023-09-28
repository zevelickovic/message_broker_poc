namespace Sks365.MessageBrokers.Configuration
{
    public class RabbitMqSubscriberSettings : RabbitMqSettings
    {
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public ushort Prefetch { get; set; } = 1;
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
    }
}
