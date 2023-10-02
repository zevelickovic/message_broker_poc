using Newtonsoft.Json;

namespace Sks365.MessageBrokers.Configuration.Broker;

public class BrokerConfigurationBuilder
{
    public static BrokerConfiguration? Get()
    {
        using (StreamReader r = new StreamReader(Path.GetFullPath("message-broker-config.json")))
        {
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<BrokerConfiguration>(json);
        }
    }
}