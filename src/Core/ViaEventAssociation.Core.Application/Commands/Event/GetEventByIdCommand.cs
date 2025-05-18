using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class GetEventByIdCommand {
        public Guid EventId { get; }
        private GetEventByIdCommand(Guid eventId) {
            EventId = eventId;
        }
        public static Result<GetEventByIdCommand> Create(string eventId) {
            if (string.IsNullOrEmpty(eventId)) {
                return new Result<GetEventByIdCommand>(1, "Event ID cannot be null or empty");
            }
            if (!Guid.TryParse(eventId, out Guid parsedEventId)) {
                return new Result<GetEventByIdCommand>(2, "Invalid Event ID format");
            }
            return new Result<GetEventByIdCommand>(new GetEventByIdCommand(parsedEventId));
        }

        public VEvent? VEvent { get; private set; }
        public Result<GetEventByIdCommand> AddResponse(VEvent vevent) {
            VEvent = vevent;
            return new Result<GetEventByIdCommand>(this);
        }
    }
}
