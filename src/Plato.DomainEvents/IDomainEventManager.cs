namespace Plato.DomainEvents
{
    public interface IDomainEventManager
    {        
        IDomainEventScope Scope();
    }
}