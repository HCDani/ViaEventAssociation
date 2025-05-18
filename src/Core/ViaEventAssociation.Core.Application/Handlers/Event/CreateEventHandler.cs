using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.Event {
    public class CreateEventHandler : ICommandHandler<UpdateEventMaxnumberOfGuestsCommand> {
        private readonly IEventRepository eventRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateEventHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) {
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<UpdateEventMaxnumberOfGuestsCommand>> HandleAsync(UpdateEventMaxnumberOfGuestsCommand command) {
            VEvent vEvent = VEvent.Create(Guid.NewGuid());
            await eventRepository.CreateAsync(vEvent);
            await unitOfWork.SaveChangesASync();
            return command.AddResponse(vEvent.Id);
        }
    }
}
