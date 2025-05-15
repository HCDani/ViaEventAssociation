using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Application.Handlers.Event;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Infrastructure.Persistence.UnitOfWork;
using ViaEventAssociation.Infrastructure.Persistence.Repositories;
using ViaEventAssociation.Core.Application.Commands.GuestNS;
using ViaEventAssociation.Core.Application.Handlers.GuestNS;

namespace ViaEventAssociation.Core.Application.AppEntry {
    public class CommandDispatcher : ICommandDispatcher {
        private readonly IEventRepository eventRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IGuestRepository guestRepository;
        private readonly IUnitOfWork unitOfWork;

        public CommandDispatcher(EFCDbContext context) {
            eventRepository = new EventRepository(context);
            locationRepository = new LocationRepository(context);
            guestRepository = new GuestRepository(context);
            unitOfWork = new UnitOfWork(context);
        }
        public Task<Result<TCommand>> DispatchAsync<TCommand>(TCommand command) {
           Type serviceType = typeof(ICommandHandler<TCommand>);
            ICommandHandler<TCommand> handler = null;
            switch (serviceType) {
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>) new CreateEventHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventDescriptionHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventDurationHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventMaxnumberOfGuestsHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventStatusHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventTitleHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventVisibilityHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventLocationHandler(eventRepository, locationRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<RegisterGuestCommand>):
                    handler = (ICommandHandler<TCommand>)new RegisterGuestHandler(guestRepository, unitOfWork);
                    break;
                // Add more cases for other command handlers as needed
                default:
                    throw new NotImplementedException($"No handler found for command type {typeof(TCommand).Name}");
            }
            return handler.HandleAsync(command);
        }
    }
}
