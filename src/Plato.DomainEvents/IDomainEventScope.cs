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
