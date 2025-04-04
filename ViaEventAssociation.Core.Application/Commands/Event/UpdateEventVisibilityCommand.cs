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
        private UpdateEventVisibilityCommand(Visibility visibility) {
            Visibility = visibility;
        }
        public static Result<UpdateEventVisibilityCommand> Create(string visibility) {
            Enum.TryParse<Visibility>(visibility, out Visibility eventVisibility);
            return new Result<UpdateEventVisibilityCommand>(new UpdateEventVisibilityCommand(eventVisibility));
        }
    }
}
