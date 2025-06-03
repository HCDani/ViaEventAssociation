using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries {
    public abstract class UpcomingEvents {
        public record Query(Guid EventId) : IQuery<UpcomingEvents.Query, UpcomingEvents.Answer>;
        public record Answer(List<UpEvent> UpEvents);
        public record UpEvent(Guid Id, string? Title, string? Decription, DateTime? From, DateTime? To, int? MaxNumberOfGuests, int? CurrentNumberOfGuests, int? Visibility);
    }
}
