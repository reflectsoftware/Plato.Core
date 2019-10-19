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
