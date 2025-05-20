using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.GuestNS {
    public class GetGuestByIdCommand {
        public Guid GuestId { get; }
        public GetGuestByIdCommand(Guid guestId) {
            GuestId = guestId;
        }
        public static Result<GetGuestByIdCommand> Create(string guestId) {
            if (string.IsNullOrEmpty(guestId)) {
                return new Result<GetGuestByIdCommand>(1, "Event ID cannot be null or empty");
            }
            if (!Guid.TryParse(guestId, out Guid parsedGuestId)) {
                return new Result<GetGuestByIdCommand>(2, "Invalid Event ID format");
            }
            return new Result<GetGuestByIdCommand>(new GetGuestByIdCommand(parsedGuestId));
        }
        
        public Guest? Guest { get; private set; }
        public Result<GetGuestByIdCommand> AddResponse(Guest guest) {
            Guest = guest;
            return new Result<GetGuestByIdCommand>(this);
        }
    }
}
