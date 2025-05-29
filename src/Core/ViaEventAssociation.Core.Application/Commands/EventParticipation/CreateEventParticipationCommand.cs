using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.EventGuestParticipation {
    public class CreateEventParticipationCommand {
        public Guid guestId { get; }
        public Guid VEventId { get; }
        public ParticipationStatus ParticipationStatus { get; }

        private CreateEventParticipationCommand(Guid vEventId, Guid guestId, ParticipationStatus participationStatus) {
            this.VEventId = vEventId;
            this.guestId = guestId;
            ParticipationStatus = participationStatus;
        }
        public static Result<CreateEventParticipationCommand> Create(string vEventId, string guestId, string status) {
            Enum.TryParse<ParticipationStatus>(status, out ParticipationStatus participationStatus);
            return new Result<CreateEventParticipationCommand>(new CreateEventParticipationCommand( Guid.Parse(vEventId), Guid.Parse(guestId), participationStatus));
        }
        public Guid ParticipationId { get; set; }
        public Result<CreateEventParticipationCommand> AddResponse(Result<EventParticipation> eventParticipation) {
            if (eventParticipation.resultCode != 0) {
                return new Result<CreateEventParticipationCommand>(eventParticipation.resultCode, eventParticipation.errorMessage);
            }
            ParticipationId = eventParticipation.payLoad.Id;
            return new Result<CreateEventParticipationCommand>(this);
        }
    }
}
