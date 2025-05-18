using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class UpdateEventStatusCommand {
        public EventStatus Status { get; }
        public Guid EventId { get; }

        private UpdateEventStatusCommand(EventStatus status, Guid eventId) {
            Status = status;
            EventId = eventId;
        }

        public static Result<UpdateEventStatusCommand> Create(string status, string eventId) {
            Enum.TryParse<EventStatus>(status, out EventStatus eventStatus);
            return new Result<UpdateEventStatusCommand>(new UpdateEventStatusCommand(eventStatus, Guid.Parse(eventId)));
        }
        public Result<UpdateEventStatusCommand> AddResponse(Result<EventStatus> status) {
            if (status.resultCode != 0) {
                return new Result<UpdateEventStatusCommand>(status.resultCode, status.errorMessage);
            }
            return new Result<UpdateEventStatusCommand>(this);
        }
    }
}
