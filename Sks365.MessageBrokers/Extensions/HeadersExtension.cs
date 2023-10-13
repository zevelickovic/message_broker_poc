using Confluent.Kafka;

namespace Sks365.MessageBrokers.Extensions
{
    internal static class HeadersExtension
    {
        public static IDictionary<string, object> GetMessageHeader(this Headers headers)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var item in headers)
            {
                var key = item.Key;
                var value = item.GetValueBytes();
                dictionary.Add(key, value);
            }
            return dictionary;
        }
    }
}
