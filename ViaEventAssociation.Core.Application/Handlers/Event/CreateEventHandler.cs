using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Features.Fake_stuff;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.Event {
    internal class CreateEventHandler : ICommandHandler<CreateEventCommand> {
        private readonly IEventRepository eventRepository;
        private readonly IUnitOfWork unitOfWork;

        internal CreateEventHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) {
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<CreateEventCommand>> HandleAsync(CreateEventCommand command) {
            VEvent vEvent = await eventRepository.GetAsync(command.EventId);
            //Result<CreateEventCommand> result = vEvent.Create(command.Create);
            return null;
        }
    }
}
