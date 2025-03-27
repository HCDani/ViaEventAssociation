namespace ViaEventAssociation.Core.Domain.Common.Bases;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; }
    
    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity &&
               Id.Equals(entity.Id);
    }
    
    public bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;
        return Id.Equals(other.Id);
    }
    
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /*
     Optionally, We could add domain event handling or other entity-related functionality here.
    */
}
