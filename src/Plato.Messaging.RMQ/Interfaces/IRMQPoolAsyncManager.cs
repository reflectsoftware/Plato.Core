// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Threading.Tasks;
using Plato.Messaging.Interfaces;

namespace Plato.Messaging.RMQ.Interfaces
{
    public interface IRMQPoolAsyncManager
    {
        Task ReadAsync(string queueName, Func<IMessageReceiveResult<byte[]>, Task> onAsyncMessage, Func<Exception, Task<bool>> onAsyncException = null, int msecTimeout = -1, int delay = 2500);
        Task ReadAsync(string queueName, Func<IMessageReceiveResult<string>, Task> onAsyncMessage, Func<Exception, Task<bool>> onAsyncException = null, int msecTimeout = -1, int delay = 2500);
        Task SendAsync(string queueName, byte[] data, Action<RMQSenderProperties> action = null);
        Task SendAsync(string queueName, object data, Action<RMQSenderProperties> action = null);
        Task SendAsync(string queueName, string data, Action<RMQSenderProperties> action = null);
    }
}