using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.EventGuestParticipation {
    public class CreateEventParticipationHandler : ICommandHandler<CreateEventParticipationCommand> {
        private readonly IGuestRepository guestRepository;
        private readonly IEventRepository eventRepository;
        private readonly IStoreEventParticipation storeEventParticipation;
        private readonly IGetEventParticipants getEventParticipants;
        private readonly IUnitOfWork unitOfWork;

        public CreateEventParticipationHandler(IGuestRepository guestRepository, IEventRepository eventRepository, IStoreEventParticipation storeEventParticipation, IGetEventParticipants getEventParticipants, IUnitOfWork unitOfWork) {
            this.guestRepository = guestRepository;
            this.eventRepository = eventRepository;
            this.storeEventParticipation = storeEventParticipation;
            this.getEventParticipants = getEventParticipants;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<CreateEventParticipationCommand>> HandleAsync(CreateEventParticipationCommand command) {
            Guest guest = await guestRepository.GetAsync(command.guestId);
            VEvent vEvent = await eventRepository.GetAsync(command.VEventId);
            Result<EventParticipation> rEventParticipation = await EventParticipation.Create(Guid.NewGuid(), guest, vEvent, command.ParticipationStatus, getEventParticipants);
            if (rEventParticipation.IsSuccess()) {
                await storeEventParticipation.CreateAsync(rEventParticipation.payLoad);
                await unitOfWork.SaveChangesASync();
            }

            return command.AddResponse(rEventParticipation);


        }
    }
}
