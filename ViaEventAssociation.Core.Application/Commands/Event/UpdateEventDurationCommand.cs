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

        private UpdateEventDurationCommand(EventDuration duration) {
            Duration = duration;
        }
        public static Result<UpdateEventDurationCommand> Create(DateTime from, DateTime to) {
            Result<EventDuration> durationResult = EventDuration.Create(from, to);
            if (durationResult.resultCode != 0) {
                return new Result<UpdateEventDurationCommand>(durationResult.resultCode, durationResult.errorMessage);
            }
            return new Result<UpdateEventDurationCommand>(new UpdateEventDurationCommand(durationResult.payLoad));
        }
    }
}
