using Plato.Configuration;
using Plato.Configuration.Interfaces;
using Plato.Messaging.AMQ;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Plato.TestHarness.ConfigurationTest
{
    public class ConfigurationPlayground
    {
        static public Task RunAsync()
        {
            // app settings
            var v1 = ConfigurationManager.AppSettings["key1"];
            var v2 = ConfigurationManager.AppSettings["key2"];

            // section settings
            var xmlConfigSection = (XmlNode)ConfigurationManager.GetSection("amqSettings");
            if (xmlConfigSection != null)
            {
                var configNode = new ConfigNode(xmlConfigSection);
                var nodeAttributes = ConfigHelper.GetNodeChildAttributes(configNode, ".");
            }

            var amqConfig = new AMQConfigurationManager();

            return Task.CompletedTask;
        }
    }
}
