using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class UpdateEventDescriptionCommand {
        public Description Description { get; }
        private UpdateEventDescriptionCommand(Description description) {
            Description = description;
        }
        public static Result<UpdateEventDescriptionCommand> Create(string description) {
            Result<Description> descriptionResult = Description.Create(description);
            if (descriptionResult.resultCode != 0) {
                return new Result<UpdateEventDescriptionCommand>(descriptionResult.resultCode, descriptionResult.errorMessage);
            }
            return new Result<UpdateEventDescriptionCommand>(new UpdateEventDescriptionCommand(descriptionResult.payLoad));
        }
    }
}
