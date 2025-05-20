using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    class UpdateEventVisibilityCommand {
        public Visibility Visibility { get; }
        public Guid EventId { get; }
        private UpdateEventVisibilityCommand(Visibility visibility, Guid eventId) {
            Visibility = visibility;
            EventId = eventId;
        }
        public static Result<UpdateEventVisibilityCommand> Create(string eventId, string visibility) {
            Enum.TryParse<Visibility>(visibility, out Visibility eventVisibility);
            return new Result<UpdateEventVisibilityCommand>(new UpdateEventVisibilityCommand(eventVisibility, Guid.Parse(eventId)));
        }
        public Result<UpdateEventVisibilityCommand> AddResponse(Result<Visibility> visibility) {
            if (visibility.resultCode != 0) {
                return new Result<UpdateEventVisibilityCommand>(visibility.resultCode, visibility.errorMessage);
            }
            return new Result<UpdateEventVisibilityCommand>(this);
        }
    }
}
