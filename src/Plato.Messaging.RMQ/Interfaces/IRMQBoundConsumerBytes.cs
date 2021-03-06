﻿// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System.Threading.Tasks;

namespace Plato.Messaging.RMQ.Interfaces
{
    public interface IRMQBoundConsumerBytes : IRMQBoundConsumer
    {
        Task OnMessageAsync(RMQReceiverResultByte result);
    }
}
