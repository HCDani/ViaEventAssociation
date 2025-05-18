using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class UpdateEventMaxnumberOfGuestsCommand {

        private UpdateEventMaxnumberOfGuestsCommand() {
        }
        public static Result<UpdateEventMaxnumberOfGuestsCommand> Create() {
            return new Result<UpdateEventMaxnumberOfGuestsCommand>(new UpdateEventMaxnumberOfGuestsCommand());
        }
        public Guid EventId { get; set; }
        public Result<UpdateEventMaxnumberOfGuestsCommand> AddResponse(Guid eventId) {
            EventId = eventId;
            return new Result<UpdateEventMaxnumberOfGuestsCommand> (this);
        }
    }
}