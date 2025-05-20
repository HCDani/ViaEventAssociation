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
    class UpdateEventStatusHandler : ICommandHandler<UpdateEventStatusCommand> {
        private readonly IEventRepository eventRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventStatusHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) {
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<UpdateEventStatusCommand>> HandleAsync(UpdateEventStatusCommand command) {
            VEvent vEvent = await eventRepository.GetAsync(command.EventId);
            Result<EventStatus> result = vEvent.UpdateStatus(command.Status);
            await unitOfWork.SaveChangesASync();
            return command.AddResponse(result);
        }
    }
}
