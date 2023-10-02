using Sks365.MessageBrokers.Brokers;

namespace Sks365.MessageBrokers.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void StartAllConsumers(this IServiceProvider services)
        {
            var consumerBroker = (IMessageBroker)services.GetService(typeof(IMessageBroker));
            consumerBroker.StartAllConsumers();
        }
    }
}