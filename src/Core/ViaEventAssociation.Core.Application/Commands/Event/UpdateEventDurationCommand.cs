using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class UpdateEventDurationCommand {
        public EventDuration Duration { get; }
        public Guid EventId { get; }
        private UpdateEventDurationCommand(EventDuration duration, Guid eventId) {
            Duration = duration;
            EventId = eventId;
        }
        public static Result<UpdateEventDurationCommand> Create(string eventId, DateTime from, DateTime to) {
            Result<EventDuration> durationResult = EventDuration.Create(from, to);
            if (durationResult.resultCode != 0) {
                return new Result<UpdateEventDurationCommand>(durationResult.resultCode, durationResult.errorMessage);
            }
            return new Result<UpdateEventDurationCommand>(new UpdateEventDurationCommand(durationResult.payLoad, Guid.Parse(eventId)));
        }
        public Result<UpdateEventDurationCommand> AddResponse(Result<EventDuration> duration) {
            if (duration.resultCode != 0) {
                return new Result<UpdateEventDurationCommand>(duration.resultCode, duration.errorMessage);
            }
            return new Result<UpdateEventDurationCommand>(this);
        }
    }
}
