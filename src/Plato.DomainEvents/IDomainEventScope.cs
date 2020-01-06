// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plato.DomainEvents
{
    public interface IDomainEventScope : IDisposable
    {
        Task AddAsync(DomainEvent domainEvent);
        Task AddRangeAsync(IEnumerable<DomainEvent> domainEvents);
        Task BroadcastAsync();
    }
}
