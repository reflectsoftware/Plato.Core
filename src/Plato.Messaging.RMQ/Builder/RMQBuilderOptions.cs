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
    public abstract class RMQBuilderOptions
    {
        public RMQConnectionSettings ConnectionSettings { get; set; }
        public Action<Exception> OnException { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Messaging.RMQ.Builder.RMQBuilderOptions" />
    public class RMQQueueBuilderOptions : RMQBuilderOptions
    {
        public RMQQueueSettings QueueSettings { get; set; }
    }

}
