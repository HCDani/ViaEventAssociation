using ViaEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.Values;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class Guest : AggregateRoot<GuestId>
{
    public Email Email { get; private set; }
    public GuestName Name { get; private set; }
    public ProfilePictureUrl ProfilePictureUrl { get; private set; }

    private Guest(GuestId id, GuestName name, Email email, ProfilePictureUrl profilePictureUrl): base(id)
    {
        Name = name;
        Email = email;
        ProfilePictureUrl = profilePictureUrl;
    }

    public static Guest RegisterGuest( GuestId id, GuestName guestName, Email email, ProfilePictureUrl profilePictureUrl)
    {
        return new Guest(id, guestName, email, profilePictureUrl);
   
    }
}
