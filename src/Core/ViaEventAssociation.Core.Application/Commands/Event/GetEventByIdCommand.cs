using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class GetEventByIdCommand {
        public Guid EventId { get; }
        public GetEventByIdCommand(Guid eventId) {
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
        public Result<GetEventByIdCommand> AddResponse(Guid eventId) {
            return new Result<GetEventByIdCommand>(this);
        }
    }
}
