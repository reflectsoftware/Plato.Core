// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.Interfaces;
using System;
using System.Threading.Tasks;

namespace Plato.Messaging.RMQ.Interfaces
{
    public interface IRMQPoolAsync: IDisposable
    {
        Task<IRMQPoolContainer<T>> GetAsync<T>(string connectionName, string queueName, string exchangeName = null) where T : IMessageReceiverSender;

    }
}