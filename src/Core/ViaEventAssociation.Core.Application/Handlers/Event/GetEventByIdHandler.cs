using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.Event {
    public class GetEventByIdHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) : ICommandHandler<GetEventByIdCommand> {
        private readonly IEventRepository eventRepository = eventRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result<GetEventByIdCommand>> HandleAsync(GetEventByIdCommand command) {
            VEvent vEvent = await eventRepository.GetAsync(command.EventId);
            return command.AddResponse(vEvent);
        }
    }
}
