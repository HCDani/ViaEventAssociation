﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.LocationNS;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.LocationNS {
    public class CreateLocationHandler : ICommandHandler<CreateLocationCommand> {
        private readonly ILocationRepository locationRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateLocationHandler(ILocationRepository locationRepository, IUnitOfWork unitOfWork) {
            this.locationRepository = locationRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<CreateLocationCommand>> HandleAsync(CreateLocationCommand command) {
            Result<Location> rLocation = Location.Create(Guid.NewGuid(), command.LocationName, command.MaxCapacity, command.Availability, command.Address);
            if (rLocation.resultCode == 0) {
                await locationRepository.CreateAsync(rLocation.payLoad);
            }
            await unitOfWork.SaveChangesASync();
            return command.AddResponse(rLocation);
        }
    }
}
