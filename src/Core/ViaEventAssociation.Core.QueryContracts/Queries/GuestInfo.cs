using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries {
    public abstract class GuestInfo {
        public record Query(Guid GuestId) : IQuery<Query, Answer>;
        public record Answer(Guid Id, string? FName,string? LName, string? Email, string? Ppurl, int? InvitationCount, int? UpComingEventCount);
    }
}
