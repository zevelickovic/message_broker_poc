using Microsoft.Extensions.Options;
using Sks365.MessageBrokers.Configuration.Broker;

namespace Sks365.MessageBrokers.TestApi.Options
{
    public class ConfigurationMessageBrokerOptions : IConfigureOptions<MessageBrokerOptions>
    {
        /// <summary>
        /// The settingsTest
        /// </summary>
        private readonly MessageBrokerSettingsTest _settingsTest;



        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationMessageBrokerOptions" /> class.
        /// </summary>
        /// <param name="settingsTest">The settingsTest.</param>
        /// <exception cref="System.ArgumentNullException">settingsTest</exception>
        public ConfigurationMessageBrokerOptions(IOptions<MessageBrokerSettingsTest> settingsTest)
        {
            this._settingsTest = settingsTest.Value ?? throw new System.ArgumentNullException(nameof(settingsTest));
        }

        /// <summary>
        /// Invoked to configure a <typeparamref name="TOptions" /> instance.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        public void Configure(MessageBrokerOptions options)
        {
            options = _settingsTest;
            options.Nesto = _settingsTest.Nesto;
        }
    }
}

public class MessageBrokerSettingsTest : MessageBrokerOptions
{

}