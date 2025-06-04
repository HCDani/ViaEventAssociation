using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries {
    public abstract class EventInfo {
        public record Query(Guid EventId) : IQuery<Query, Answer>;
        public record Answer(Guid? Id, string? Title, string? Description, DateTime? From, DateTime? To, int? MaxNumberOfGuests, int? CurrentNumberOfGuests, string? LocationName,
            int? Visibility,
            List<GuestDetails> GuestDetailsList);
        public record GuestDetails(Guid? Id, string? FirstName, string? LastName, string? ProfilePictureUrl);
    }
}
