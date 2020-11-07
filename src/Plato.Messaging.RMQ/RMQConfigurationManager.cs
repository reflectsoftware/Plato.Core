// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Newtonsoft.Json;
using Plato.Configuration;
using Plato.Messaging.RMQ.Interfaces;
using Plato.Messaging.RMQ.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Plato.Messaging.RMQ
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Messaging.RMQ.Interfaces.IRMQConfigurationManager" />
    public class RMQConfigurationManager : IRMQConfigurationManager
    {        
        public RMQConfigSettings Settings { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RMQConfigurationManager"/> class.
        /// </summary>
        /// <param name="configPath">The configuration path.</param>
        public RMQConfigurationManager(string configPath = null)
        {
            Settings = new RMQConfigSettings();

            if (configPath == null)
            {
                var xmlFile = $"{AppDomain.CurrentDomain.BaseDirectory}RMQSettings.config";
                var jsonFile = $"{AppDomain.CurrentDomain.BaseDirectory}RMQSettings.json";

                if (File.Exists(xmlFile))
                {
                    configPath = xmlFile;
                }
                else if(File.Exists(jsonFile))
                {
                    configPath = jsonFile;
                }
            }

            if (configPath != null)
            {
                if(Path.GetExtension(configPath).ToLower() == ".json")
                {
                    var json = File.ReadAllText(configPath);
                    Settings = JsonConvert.DeserializeObject<RMQConfigSettings>(json);
                }
                else // XML legacy
                {
                    using (var configContainer = new ConfigContainer(configPath, "./rmqSettings"))
                    {
                        var configNode = configContainer.Node;

                        var settingNodes = configNode.GetConfigNodes($"./connectionSettings");
                        foreach (var settingNode in settingNodes)
                        {
                            var connectionSettings = new RMQConnectionSettings();
                            Settings.Connections.Add(connectionSettings);

                            connectionSettings.Name = settingNode.GetAttribute(".", "name", "");
                            connectionSettings.Username = settingNode.GetAttribute(".", "username", "");
                            connectionSettings.Password = settingNode.GetAttribute(".", "password", "");
                            connectionSettings.VirtualHost = settingNode.GetAttribute(".", "virtualhost", "/");
                            connectionSettings.Uri = settingNode.GetAttribute(".", "uri", "amqp://localhost:5672");
                            connectionSettings.DelayOnReconnect = int.Parse(settingNode.GetAttribute(".", "delayOnReconnect", "1000"));
                            connectionSettings.ForceReconnectionTime = TimeSpan.FromMinutes(int.Parse(settingNode.GetAttribute(".", "forceReconnectionTime", "0")));
                        }

                        var exchangeNodes = configNode.GetConfigNodes($"./exchange");
                        foreach (var exchangeNode in exchangeNodes)
                        {
                            var exchangeSettings = new RMQExchangeSettings(exchangeNode.GetAttribute(".", "name", ""));
                            Settings.Exchanges.Add(exchangeSettings);

                            exchangeSettings.ExchangeName = exchangeNode.GetAttribute(".", "exchangeName", "");
                            exchangeSettings.Type = exchangeNode.GetAttribute(".", "type", "direct");
                            exchangeSettings.Durable = exchangeNode.GetAttribute(".", "durable", "true") == "true";
                            exchangeSettings.AutoDelete = exchangeNode.GetAttribute(".", "autoDelete", "false") == "true";

                            var argsNode = configNode.GetConfigNode($"./exchange[@name='{exchangeSettings.Name}']/arguments");
                            if (argsNode != null)
                            {
                                var attributes = argsNode.GetAttributes();
                                foreach (var key in attributes.AllKeys)
                                {
                                    exchangeSettings.Arguments[key] = attributes[key];
                                }
                            }
                        }

                        var queueNodes = configNode.GetConfigNodes($"./queue");
                        foreach (var queueNode in queueNodes)
                        {
                            var queueSettings = new RMQQueueSettings(queueNode.GetAttribute(".", "name", ""));
                            Settings.Queues.Add(queueSettings);

                            queueSettings.QueueName = queueNode.GetAttribute(".", "queueName", "");
                            queueSettings.Durable = queueNode.GetAttribute(".", "durable", "true") == "true";
                            queueSettings.Exclusive = queueNode.GetAttribute(".", "exclusive", "false") == "true";
                            queueSettings.AutoDelete = queueNode.GetAttribute(".", "autoDelete", "false") == "true";
                            queueSettings.Persistent = queueNode.GetAttribute(".", "persistent", "true") == "true";

                            var routingKeys = queueNode.GetAttribute(".", "routingKeys", "");
                            foreach (var key in routingKeys.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                queueSettings.RoutingKeys.Add(key);
                            }

                            var argsNode = configNode.GetConfigNode($"./queue[@name='{queueSettings.Name}']/arguments");
                            if (argsNode != null)
                            {
                                var attributes = argsNode.GetAttributes();
                                foreach (var key in attributes.AllKeys)
                                {
                                    queueSettings.Arguments[key] = attributes[key];
                                }
                            }

                            // get consumer info if it exists
                            var consumerNode = configNode.GetConfigNode($"./queue[@name='{queueSettings.Name}']/consumer");
                            if (consumerNode != null)
                            {
                                queueSettings.ConsumerSettings.Tag = queueNode.GetAttribute(".", "tag", Guid.NewGuid().ToString());
                                queueSettings.ConsumerSettings.Exclusive = queueNode.GetAttribute(".", "exclusive", "false") == "true";
                                queueSettings.ConsumerSettings.NoAck = queueNode.GetAttribute(".", "noAck", "true") == "true";
                                queueSettings.ConsumerSettings.NoLocal = queueNode.GetAttribute(".", "noLocal", "true") == "true";
                            }
                        }
                    }
                }
            }

            foreach (var connection in Settings.Connections)
            {
                foreach (var uri in connection.Uri.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    connection.Endpoints.Add(uri);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RMQConfigurationManager"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public RMQConfigurationManager(RMQConfigSettings settings)
        {
            Settings = settings;

            foreach (var connection in Settings.Connections)
            {
                connection.Endpoints.Clear();
                foreach (var uri in connection.Uri.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    connection.Endpoints.Add(uri);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RMQConfigurationManager" /> class.
        /// </summary>
        /// <param name="connections">The connections.</param>
        /// <param name="queueSettings">The queue settings.</param>
        public RMQConfigurationManager(
            IEnumerable<RMQConnectionSettings> connections, 
            IEnumerable<RMQExchangeSettings> exchangeSettings = null,
            IEnumerable<RMQQueueSettings> queueSettings = null) : this()
        {
            if (connections != null)
            {
                Settings.Connections = connections.ToList();

                foreach (var connection in Settings.Connections)
                {
                    connection.Endpoints.Clear();

                    foreach (var uri in connection.Uri.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        connection.Endpoints.Add(uri);
                    }
                }
            }

            Settings.Exchanges = exchangeSettings?.ToList() ?? Settings.Exchanges;
            Settings.Queues = queueSettings?.ToList() ?? Settings.Queues;
        }

        /// <summary>
        /// Gets the connection settings.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public RMQConnectionSettings GetConnectionSettings(string name)
        {
            var settings = Settings.Connections.FirstOrDefault(x => x.Name == name);
            return settings;
        }

        /// <summary>
        /// Gets the exchange settings.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public RMQExchangeSettings GetExchangeSettings(string name, IDictionary<string, object> arguments = null)
        {
            var settings = Settings.Exchanges.FirstOrDefault(x => x.Name == name);

            if (arguments != null)
            {
                settings.Arguments.Clear();
                foreach (var arg in arguments)
                {
                    settings.Arguments[arg.Key] = arg.Value;
                }
            }

            return settings;
        }

        /// <summary>
        /// Gets the queue settings.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public RMQQueueSettings GetQueueSettings(string name, IDictionary<string, object> arguments = null)
        {
            var settings = Settings.Queues.FirstOrDefault(x => x.Name == name);

            if (arguments != null)
            {
                settings.Arguments.Clear();
                foreach (var arg in arguments)
                {
                    settings.Arguments[arg.Key] = arg.Value;
                }
            }

            return settings;
        }
    }
}
