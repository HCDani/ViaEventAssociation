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
        private UpdateEventMaxNumberOfGuestsCommand(MaxNumberOfGuests maxNumberOfGuests) {
            MaxNumberOfGuests = maxNumberOfGuests;
        }
        public static Result<UpdateEventMaxNumberOfGuestsCommand> Create(string maxNumberOfGuests) {
            Int32.TryParse(maxNumberOfGuests, out int iNumberOfGuests);
            Result<MaxNumberOfGuests> maxNumberOfGuestsResult = MaxNumberOfGuests.Create(iNumberOfGuests);
            if (maxNumberOfGuestsResult.resultCode != 0) {
                return new Result<UpdateEventMaxNumberOfGuestsCommand>(maxNumberOfGuestsResult.resultCode, maxNumberOfGuestsResult.errorMessage);
            }
            return new Result<UpdateEventMaxNumberOfGuestsCommand>(new UpdateEventMaxNumberOfGuestsCommand(maxNumberOfGuestsResult.payLoad));
        }
    }
}
