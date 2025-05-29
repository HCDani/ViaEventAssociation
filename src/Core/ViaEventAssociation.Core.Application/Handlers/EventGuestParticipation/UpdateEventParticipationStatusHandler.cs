using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.EventGuestParticipation {
    public class UpdateEventParticipationStatusHandler : ICommandHandler<UpdateEventParticipationStatusCommand>{
        private readonly IGetEventParticipants getEventParticipants;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventParticipationStatusHandler(IGetEventParticipants getEventParticipants, IUnitOfWork unitOfWork) {
            this.getEventParticipants = getEventParticipants;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<UpdateEventParticipationStatusCommand>> HandleAsync(UpdateEventParticipationStatusCommand command) {
            List<EventParticipation> eventParticipations = await getEventParticipants.GetParticipants(command.EventId);
            EventParticipation? participant = eventParticipations.Find(participant => participant.Guest.Id == command.GuestId);
            if (participant != null) {
                Result<ParticipationStatus> updateResult = await participant.UpdateStatus(command.Status, getEventParticipants);
                if (updateResult.IsSuccess()) {
                    await unitOfWork.SaveChangesASync();
                }
                return command.AddResponse(updateResult);
            } else { 
                return command.AddResponse(new Result<ParticipationStatus>(151, "Guest is not participating in the event."));
            }

        }

    }
}
