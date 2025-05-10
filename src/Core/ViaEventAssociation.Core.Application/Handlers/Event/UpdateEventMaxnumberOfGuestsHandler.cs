using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Common.FakeStuff;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.Event {
    class UpdateEventMaxnumberOfGuestsHandler : ICommandHandler<UpdateEventMaxNumberOfGuestsCommand> {
        private readonly IEventRepository eventRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventMaxnumberOfGuestsHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) {
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<UpdateEventMaxNumberOfGuestsCommand>> HandleAsync(UpdateEventMaxNumberOfGuestsCommand command) {
            VEvent vEvent = await eventRepository.GetAsync(command.EventId);
            Result<MaxNumberOfGuests> result = vEvent.UpdateMaxNumberOfGuests(command.MaxNumberOfGuests);
            return command.AddResponse(result);
        }
    }
}
