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
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;

namespace ViaEventAssociation.Core.Application.Handlers.Event {
    class UpdateEventLocationHandler : ICommandHandler<UpdateEventLocationCommand> {
        private readonly IEventRepository eventRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventLocationHandler(IEventRepository eventRepository, ILocationRepository locationRepository, IUnitOfWork unitOfWork) {
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
            this.locationRepository = locationRepository;
        }
        public async Task<Result<UpdateEventLocationCommand>> HandleAsync(UpdateEventLocationCommand command) {
            VEvent vEvent = await eventRepository.GetAsync(command.EventId);
            Location location = await locationRepository.GetAsync(command.LocationId);
            Result<Location> result = vEvent.UpdateLocation(location);
            return command.AddResponse(result);
        }
    }
}
