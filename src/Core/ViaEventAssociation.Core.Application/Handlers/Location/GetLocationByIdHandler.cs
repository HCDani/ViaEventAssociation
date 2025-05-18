using System;
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
    public class GetLocationByIdHandler : ICommandHandler<GetLocationByIdCommand> {
        private readonly ILocationRepository locationRepository;
        private readonly IUnitOfWork unitOfWork;
        public GetLocationByIdHandler(ILocationRepository locationRepository, IUnitOfWork unitOfWork) {
            this.locationRepository = locationRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<GetLocationByIdCommand>> HandleAsync(GetLocationByIdCommand command) {
            Location location = await locationRepository.GetAsync(command.LocationId);
            await unitOfWork.SaveChangesASync();
            return command.AddResponse(location.Id);
        }
    }
}
