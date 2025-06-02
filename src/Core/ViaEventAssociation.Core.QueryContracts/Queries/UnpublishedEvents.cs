using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries {
    public abstract class UnpublishedEvents {
        public record Query : IQuery<UnpublishedEvents.Query, UnpublishedEvents.Answer>;
        public record Answer(List<uEvent> uEvents);
        public record uEvent(Guid Id, string? Title,int? Status);
    }
}
