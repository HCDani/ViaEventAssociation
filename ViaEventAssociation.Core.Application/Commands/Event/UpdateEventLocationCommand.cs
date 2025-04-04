using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class UpdateEventLocationCommand {
        public Guid LocationId { get; }
        public Guid EventId { get; }
        private UpdateEventLocationCommand(Guid eventId, Guid locationId) {
            LocationId = locationId;
            EventId = eventId;
        }
        public static Result<UpdateEventLocationCommand> Create(string eventId, string locationId )  {
            return new Result<UpdateEventLocationCommand>(new UpdateEventLocationCommand(Guid.Parse(eventId), Guid.Parse(locationId)));
        }
    }
}
