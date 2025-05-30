﻿using System;
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
using ViaEventAssociation.Core.Application.Commands.LocationNS;
using ViaEventAssociation.Core.Application.Handlers.LocationNS;
using ViaEventAssociation.Core.Application.Commands.EventGuestParticipation;
using ViaEventAssociation.Core.Application.Handlers.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts;
using ViaEventAssociation.Infrastructure.Persistence.Contracts;


namespace ViaEventAssociation.Core.Application.AppEntry {
    public class CommandDispatcher : ICommandDispatcher {
        private readonly IEventRepository eventRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IGuestRepository guestRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly EventParticipantsContract eventParticipantsContract;

        public CommandDispatcher(EFCDbContext context) {
            eventRepository = new EventRepository(context);
            locationRepository = new LocationRepository(context);
            guestRepository = new GuestRepository(context);
            unitOfWork = new UnitOfWork(context);
            eventParticipantsContract = new EventParticipantsContract(context);
        }
        public Task<Result<TCommand>> DispatchAsync<TCommand>(TCommand command) {
           Type serviceType = typeof(ICommandHandler<TCommand>);
            ICommandHandler<TCommand> handler = null;
            switch (serviceType) {
                case Type t when t == typeof(ICommandHandler<CreateEventCommand>):
                    handler = (ICommandHandler<TCommand>) new CreateEventHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<UpdateEventDescriptionCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventDescriptionHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<UpdateEventDurationCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventDurationHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<UpdateEventMaxNumberOfGuestsCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventMaxnumberOfGuestsHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<UpdateEventStatusCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventStatusHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<UpdateEventTitleCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventTitleHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<UpdateEventVisibilityCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventVisibilityHandler(eventRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<UpdateEventLocationCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventLocationHandler(eventRepository, locationRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<GetEventByIdCommand>):
                    handler = (ICommandHandler<TCommand>)new GetEventByIdHandler(eventRepository,unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<RegisterGuestCommand>):
                    handler = (ICommandHandler<TCommand>)new RegisterGuestHandler(guestRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<GetGuestByIdCommand>):
                    handler = (ICommandHandler<TCommand>)new GetGuestByIdHandler(guestRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateLocationCommand>):
                    handler = (ICommandHandler<TCommand>)new CreateLocationHandler(locationRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<GetLocationByIdCommand>):
                    handler = (ICommandHandler<TCommand>)new GetLocationByIdHandler(locationRepository, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<CreateEventParticipationCommand>):
                    handler = (ICommandHandler<TCommand>)new CreateEventParticipationHandler(guestRepository,eventRepository, eventParticipantsContract, eventParticipantsContract, unitOfWork);
                    break;
                case Type t when t == typeof(ICommandHandler<UpdateEventParticipationStatusCommand>):
                    handler = (ICommandHandler<TCommand>)new UpdateEventParticipationStatusHandler(eventParticipantsContract, unitOfWork);
                    break;
                // Add more cases for other command handlers as needed
                default:
                    throw new NotImplementedException($"No handler found for command type {typeof(TCommand).Name}");
            }
            return handler.HandleAsync(command);
        }
    }
}
