using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.EventGuestParticipation {
    public class UpdateEventParticipationStatusCommand {
        public ParticipationStatus Status { get; }
        public Guid EventParticipationId { get; }
        public Guid EventId { get; }
        public Guid GuestId { get; }
        private UpdateEventParticipationStatusCommand(ParticipationStatus status, Guid eventParticipationId, Guid eventId, Guid guestId) {
            Status = status;
            EventParticipationId = eventParticipationId;
            EventId = eventId;
            GuestId = guestId;
        }
        public static Result<UpdateEventParticipationStatusCommand> Create(string eventParticipationId, string status, string eventId, string guestId) {
            Enum.TryParse<ParticipationStatus>(status, out ParticipationStatus participationStatus);
            return new Result<UpdateEventParticipationStatusCommand>(new UpdateEventParticipationStatusCommand(participationStatus, Guid.Parse(eventParticipationId), Guid.Parse(eventId), Guid.Parse(eventId)));
        }
        public Result<UpdateEventParticipationStatusCommand> AddResponse(Result<ParticipationStatus> status) {
            if (status.resultCode != 0) {
                return new Result<UpdateEventParticipationStatusCommand>(status.resultCode, status.errorMessage);
            }
            return new Result<UpdateEventParticipationStatusCommand>(this);
        }
    }
}
