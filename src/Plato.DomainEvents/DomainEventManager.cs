// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using MediatR;
using Plato.Miscellaneous;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plato.DomainEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainEventManager : IDomainEventManager
    {
        private readonly List<DomainEventScope> _scopes;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventManager" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public DomainEventManager(IMediator mediator)
        {
            Guard.AgainstNull(() => mediator);

            _scopes = new List<DomainEventScope>();
            _mediator = mediator;
        }

        /// <summary>
        /// Scopes the specified scope.
        /// </summary>
        /// <returns></returns>
        public IDomainEventScope Scope()
        {
            var eventScope = new DomainEventScope(this);
            _scopes.Add(eventScope);

            return eventScope;
        }

        /// <summary>
        /// Un-scopes the specified scope.
        /// </summary>
        /// <param name="scope">The scope.</param>
        internal async Task UnscopeAsync(DomainEventScope scope)
        {
            if (ReferenceEquals(scope, _scopes[0]))
            {
                foreach (var eventScope in _scopes)
                {
                    var events = eventScope.Events;
                    if (events.Any())
                    {
                        await _mediator.Publish(new DomainEventNotification(eventScope.Events));
                    }
                }

                _scopes.Clear();
            }
        }
    }
}
