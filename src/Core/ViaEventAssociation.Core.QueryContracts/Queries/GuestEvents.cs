using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries {
    public abstract class GuestEvents {

        public record Query(Guid GuestId) : IQuery<GuestEvents.Query,GuestEvents.Answer>;
        public record Answer(List<EventInfo> Events);
        public record EventInfo(Guid Id, string? Title, string? Description, DateTime? From, DateTime? To, int? MaxNumberOfGuests, int? CurrentNumberOfGuests);

    }
}
