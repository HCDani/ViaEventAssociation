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

        private UpdateEventStatusCommand(EventStatus status) {
            Status = status;
        }

        public static Result<UpdateEventStatusCommand> Create(string status) {
            Enum.TryParse<EventStatus>(status, out EventStatus eventStatus);
            return new Result<UpdateEventStatusCommand>(new UpdateEventStatusCommand(eventStatus));
        }
    }
}
