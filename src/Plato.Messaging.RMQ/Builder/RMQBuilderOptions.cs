// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.RMQ.Settings;
using System;

namespace Plato.Messaging.RMQ.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public class RMQBuilderConsumerOptions
    {
        public RMQConnectionSettings ConnectionSettings { get; set; }        
        public RMQQueueSettings QueueSettings { get; set; }
    }

    public class RMQBuilderSubscriptionOptions : RMQBuilderConsumerOptions
    {
        public RMQExchangeSettings ExchangeSettings { get; set; }
    }
}
