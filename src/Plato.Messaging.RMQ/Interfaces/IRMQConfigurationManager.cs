// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.RMQ.Settings;
using System.Collections.Generic;

namespace Plato.Messaging.RMQ.Interfaces
{
    public interface IRMQConfigurationManager
    {
        /// <summary>
        /// Gets the connection settings.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        RMQConnectionSettings GetConnectionSettings(string name);

        /// <summary>
        /// Gets the exchange settings.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        RMQExchangeSettings GetExchangeSettings(string name, IDictionary<string, object> arguments = null);

        /// <summary>
        /// Gets the queue settings.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        RMQQueueSettings GetQueueSettings(string name, IDictionary<string, object> arguments = null);
    }
}
