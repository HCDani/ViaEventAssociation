using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Application.Handlers.Event;
using ViaEventAssociation.Core.Domain.Common.FakeStuff;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.AppEntry {
    public class CommandDispatcher : ICommandDispatcher {
        private readonly IEventRepository eventRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IGuestRepository guestRepository;
        private readonly IUnitOfWork unitOfWork;

        public CommandDispatcher() {
            eventRepository = new InMemEventRepoStub();
            unitOfWork = new UnitOfWork();
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
                // Add more cases for other command handlers as needed
                default:
                    throw new NotImplementedException($"No handler found for command type {typeof(TCommand).Name}");
            }
            return handler.HandleAsync(command);
        }
    }
}
