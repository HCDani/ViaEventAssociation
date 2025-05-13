using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.Event {
    class UpdateEventDurationHandler : ICommandHandler<UpdateEventDurationCommand> {
        private readonly IEventRepository eventRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventDurationHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) {
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<UpdateEventDurationCommand>> HandleAsync(UpdateEventDurationCommand command) {
            VEvent vEvent = await eventRepository.GetAsync(command.EventId);
            Result<EventDuration> result = vEvent.UpdateDuration(command.Duration);
            return command.AddResponse(result);
        }
    }
}
