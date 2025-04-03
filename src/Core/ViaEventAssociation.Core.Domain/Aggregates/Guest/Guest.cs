using ViaEventAssociation.Core.Domain.Aggregates.GuestNS.Values;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.GuestNS {

    public class Guest : AggregateRoot<Guid> {
        public Email Email { get; private set; }
        public GuestName Name { get; private set; }
        public ProfilePictureUrl ProfilePictureUrl { get; private set; }

        private Guest(Guid id, GuestName name, Email email, ProfilePictureUrl profilePictureUrl) : base(id) {
            Name = name;
            Email = email;
            ProfilePictureUrl = profilePictureUrl;
        }

        public static Result<Guest> RegisterGuest(Guid id, GuestName guestName, Email email, ProfilePictureUrl profilePictureUrl) {
            if (guestName == null) return new Result<Guest>(134, "Guest name cannot be empty.");
            if (email == null) return new Result<Guest>(120, "Email cannot be empty.");
            if (profilePictureUrl == null) return new Result<Guest>(140, "Profile picture URL cannot be empty.");
            return new Result<Guest>(new Guest(id, guestName, email, profilePictureUrl));
        }
    }
}
