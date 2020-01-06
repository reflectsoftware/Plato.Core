// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using MediatR;
using System.Collections.Generic;

namespace Plato.DomainEvents
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MediatR.INotification" />
    public class DomainEventNotification : INotification
    {
        public IEnumerable<DomainEvent> Events { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventNotification" /> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public DomainEventNotification(IEnumerable<DomainEvent> events)
        {
            Events = events;
        }
    }
}
