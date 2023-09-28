using Microsoft.Extensions.Configuration;

namespace Sks365.MessageBrokers.Configuration
{
    interface IBrokerConfiguration
    {
        public IConfiguration GetProducer();
        public IConfiguration GetConsumer();
        public void SetProducer(string key, string value);
        public void SetConsumer(string key, string value);
        public string FindProducerValue(string key);
        public string FindConsumerValue(string key);
    }
}
