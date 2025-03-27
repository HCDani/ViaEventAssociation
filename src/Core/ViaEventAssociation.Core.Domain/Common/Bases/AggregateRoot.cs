namespace ViaEventAssociation.Core.Domain.Common.Bases;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    // We may include a collection of domain events,
    // or methods for ensuring internal consistency
    // across the entire aggregate here.
}