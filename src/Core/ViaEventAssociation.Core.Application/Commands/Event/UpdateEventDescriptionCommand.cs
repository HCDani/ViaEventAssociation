using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class UpdateEventDescriptionCommand {
        public Description Description { get; }
        public Guid EventId { get; }
        private UpdateEventDescriptionCommand(Description description, Guid eventId) {
            Description = description;
            EventId = eventId;
        }
        public static Result<UpdateEventDescriptionCommand> Create(string eventId, string description) {
            Result<Description> descriptionResult = Description.Create(description);
            if (descriptionResult.resultCode != 0) {
                return new Result<UpdateEventDescriptionCommand>(descriptionResult.resultCode, descriptionResult.errorMessage);
            }
            return new Result<UpdateEventDescriptionCommand>(new UpdateEventDescriptionCommand(descriptionResult.payLoad, Guid.Parse(eventId)));
        }
        public Result<UpdateEventDescriptionCommand> AddResponse(Result<Description> description) {
            if (description.resultCode != 0) {
                return new Result<UpdateEventDescriptionCommand>(description.resultCode, description.errorMessage);
            }
            return new Result<UpdateEventDescriptionCommand>(this);
        }
    }
}
