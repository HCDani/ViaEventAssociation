using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Common.Fake_stuff;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.Event {
    class UpdateEventDescriptionHandler : ICommandHandler<UpdateEventDescriptionCommand> {
        private readonly IEventRepository eventRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventDescriptionHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) {
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<UpdateEventDescriptionCommand>> HandleAsync(UpdateEventDescriptionCommand command) {
            VEvent vEvent = await eventRepository.GetAsync(command.EventId);
            Result<Description> result = vEvent.UpdateDescription(command.Description);
            return command.AddResponse(result);
        }
    }
}
