// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plato.DomainEvents
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.DomainEvents.IDomainEventScope" />
    internal class DomainEventScope : IDomainEventScope
    {
        #region declarations
        class DomainEventNode
        {
            public DomainEvent Event { get; set; }
            public bool Broadcast { get; set; }
        }
        #endregion declarations

        private readonly DomainEventManager _eventManager;
        private readonly List<DomainEventNode> _events;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventScope" /> class.
        /// </summary>
        /// <param name="eventManager">The event manager.</param>
        internal DomainEventScope(DomainEventManager eventManager)
        {
            Guard.AgainstNull(() => eventManager);

            _eventManager = eventManager;
            _events = new List<DomainEventNode>();
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns></returns>
        public Task AddAsync(DomainEvent domainEvent)
        {
            _events.Add(new DomainEventNode { Event = domainEvent, Broadcast = false });
            return Task.CompletedTask;
        }

        /// <summary>
        /// Adds the range asynchronous.
        /// </summary>
        /// <param name="domainEvents">The domain events.</param>
        /// <returns></returns>
        public Task AddRangeAsync(IEnumerable<DomainEvent> domainEvents)
        {
            _events.AddRange(domainEvents.Select(x => new DomainEventNode { Event = x, Broadcast = false }));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Broadcasts the asynchronous.
        /// </summary>
        public Task BroadcastAsync()
        {
            _events.ForEach(x => x.Broadcast = true);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        internal IEnumerable<DomainEvent> Events
        {
            get { return _events.Where(x => x.Broadcast).Select(x => x.Event); }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            _eventManager.UnscopeAsync(this).GetAwaiter().GetResult();
        }
    }
}
