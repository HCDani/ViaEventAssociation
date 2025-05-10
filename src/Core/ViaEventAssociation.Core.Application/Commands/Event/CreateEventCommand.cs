using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class CreateEventCommand {

        private CreateEventCommand() {
        }
        public static Result<CreateEventCommand> Create() {
            return new Result<CreateEventCommand>(new CreateEventCommand());
        }
        public Guid EventId { get; set; }
        public Result<CreateEventCommand> AddResponse(Guid eventId) {
            EventId = eventId;
            return new Result<CreateEventCommand> (this);
        }
    }
}