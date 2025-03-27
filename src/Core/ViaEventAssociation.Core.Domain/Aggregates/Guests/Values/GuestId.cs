using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests.Values;

public record GuestId
{
    public Guid Value { get; init; }
    
    
    public static Result<GuestId> Create(Guid value)
    {
        List<string> errors = Validate();

        return errors.Any() ? new Result<GuestId>(errors) : new Result<GuestId>(new GuestId() { Value = value });
    }

    private static List<string> Validate()
    {
        List<string> errors = new();
        return errors;
    }
}


