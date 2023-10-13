using Microsoft.Extensions.Options;
using Sks365.MessageBrokers.Configuration.Broker;
using Sks365.MessageBrokers.Configuration.Kafka;
using Sks365.MessageBrokers.Configuration.RabbitMq;

namespace Sks365.MessageBrokers.TestApi.Options
{
    public class ConfigurationMessageBrokerOptions : IConfigureOptions<MessageBrokerOptions>
    {
        /// <summary>
        /// The settingsTest
        /// </summary>
        private readonly MessageBrokerSettingsExample _settingsExample;



        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationMessageBrokerOptions" /> class.
        /// </summary>
        /// <param name="settingsTest">The settingsTest.</param>
        /// <exception cref="System.ArgumentNullException">settingsTest</exception>
        public ConfigurationMessageBrokerOptions(IOptions<MessageBrokerSettingsExample> settingsTest)
        {
            this._settingsExample = settingsTest.Value ?? throw new System.ArgumentNullException(nameof(settingsTest));
        }

        /// <summary>
        /// Invoked to configure a <typeparamref name="TOptions" /> instance.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        public void Configure(MessageBrokerOptions options)
        {
            options.RabbitMq = new RabbitMqConfiguration();
            options.RabbitMq.Subscribers = new List<RabbitMqSubscriberConfiguration>();
            options.RabbitMq.Producers = new List<RabbitMqProducerConfiguration>();
            options.Kafka = new KafkaConfiguration();
            options.Kafka.Subscribers = new List<KafkaSubscriberConfiguration>();
            options.Kafka.Producers = new List<KafkaProducerConfiguration>();
            foreach (var rabbitMq in _settingsExample.RabbitMq.Subscribers)
            {
                var subscriberConfiguration = new RabbitMqSubscriberConfiguration()
                {
                    Name = rabbitMq.Name,
                    AutoDelete = rabbitMq.AutoDelete,
                    Durable = rabbitMq.Durable,
                    Exchange = rabbitMq.Exchange,
                    Exclusive = rabbitMq.Exclusive,
                    Host = rabbitMq.Host,
                    Password = rabbitMq.Password,
                    Prefetch = rabbitMq.Prefetch,
                    Queue = rabbitMq.Queue,
                    RoutingKey = rabbitMq.RoutingKey,
                    UserName = rabbitMq.UserName,
                    VirtualHost = rabbitMq.VirtualHost
                };
                options.RabbitMq.Subscribers.Add(subscriberConfiguration);
            }
            foreach (var rabbitMq in _settingsExample.RabbitMq.Producers)
            {
                var producerConfiguration = new RabbitMqProducerConfiguration()
                {
                    Name = rabbitMq.Name,
                    Exchange = rabbitMq.Exchange,
                    Host = rabbitMq.Host,
                    Password = rabbitMq.Password,
                    UserName = rabbitMq.UserName,
                    VirtualHost = rabbitMq.VirtualHost
                };
                options.RabbitMq.Producers.Add(producerConfiguration);
            }

            foreach (var kafka in _settingsExample.Kafka.Subscribers)
            {
                var subscriberConfiguration = new KafkaSubscriberConfiguration()
                {
                    Name = kafka.Name,
                    Config = new Dictionary<string, string>(),
                    Topic = kafka.Topic,
                };
                foreach(var item in kafka.Config)
                    subscriberConfiguration.Config.Add(item.Key, item.Value);
                options.Kafka.Subscribers.Add(subscriberConfiguration);
            }
            foreach (var kafka in _settingsExample.Kafka.Producers)
            {
                var producerConfiguration = new KafkaProducerConfiguration()
                {
                    Name = kafka.Name,
                    Config = new Dictionary<string, string>(),
                    Topic = kafka.Topic,
                };
                foreach (var item in kafka.Config)
                    producerConfiguration.Config.Add(item.Key, item.Value);
                options.Kafka.Producers.Add(producerConfiguration);
            }
        }
    }
}