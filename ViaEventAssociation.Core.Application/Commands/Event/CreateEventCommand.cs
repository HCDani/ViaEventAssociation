using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class CreateEventCommand {
        public Guid EventId { get; }
        public EventStatus Status { get; }

        private CreateEventCommand(Guid eventId, EventStatus status) {
            this.EventId = eventId;
            Status = status;
        }
        public static Result<CreateEventCommand> Create(string eventId, string status) {
            Enum.TryParse<EventStatus>(status, out EventStatus eventStatus);
            return new Result<CreateEventCommand>(new CreateEventCommand(Guid.Parse(eventId), eventStatus));
        }
    }
}