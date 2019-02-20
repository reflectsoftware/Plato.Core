// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

namespace Plato.Messaging.RMQ.Interfaces
{
    public interface IRMQPoolFactory
    {
        IRMQPoolAsync CreateAsyncPool(IRMQConfigurationManager configuration, int maxGrowSize = 3);
        IRMQPool CreatePool(IRMQConfigurationManager configuration, int maxGrowSize = 3);
    }
}