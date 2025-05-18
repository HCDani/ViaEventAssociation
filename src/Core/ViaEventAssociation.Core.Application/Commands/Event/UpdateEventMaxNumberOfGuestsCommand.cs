using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class UpdateEventMaxNumberOfGuestsCommand {
        public MaxNumberOfGuests MaxNumberOfGuests { get; }
        public Guid EventId { get; }
        private UpdateEventMaxNumberOfGuestsCommand(MaxNumberOfGuests maxNumberOfGuests, Guid eventId) {
            MaxNumberOfGuests = maxNumberOfGuests;
            EventId = eventId;
        }
        public static Result<UpdateEventMaxNumberOfGuestsCommand> Create(string maxNumberOfGuests, string eventId) {
            Int32.TryParse(maxNumberOfGuests, out int iNumberOfGuests);
            Result<MaxNumberOfGuests> maxNumberOfGuestsResult = MaxNumberOfGuests.Create(iNumberOfGuests);
            if (maxNumberOfGuestsResult.resultCode != 0) {
                return new Result<UpdateEventMaxNumberOfGuestsCommand>(maxNumberOfGuestsResult.resultCode, maxNumberOfGuestsResult.errorMessage);
            }
            return new Result<UpdateEventMaxNumberOfGuestsCommand>(new UpdateEventMaxNumberOfGuestsCommand(maxNumberOfGuestsResult.payLoad, Guid.Parse(eventId)));
        }
        public Result<UpdateEventMaxNumberOfGuestsCommand> AddResponse(Result<MaxNumberOfGuests> maxNumberOfGuests) {
            if (maxNumberOfGuests.resultCode != 0) {
                return new Result<UpdateEventMaxNumberOfGuestsCommand>(maxNumberOfGuests.resultCode, maxNumberOfGuests.errorMessage);
            }
            return new Result<UpdateEventMaxNumberOfGuestsCommand>(this);
        }
    }
}
