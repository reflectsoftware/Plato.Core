// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.RMQ.Settings;
using System;

namespace Plato.Messaging.RMQ.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public class RMQBuilderOptions
    {
        public RMQConnectionSettings ConnectionSettings { get; set; }
        public Action<Exception> OnException { get; set; }
        public RMQExchangeSettings ExchangeSettings { get; set; }
        public RMQQueueSettings QueueSettings { get; set; }
    }
}
