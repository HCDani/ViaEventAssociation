using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.GuestNS;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Handlers.GuestNS {
    public class GetGuestByIdHandler : ICommandHandler<GetGuestByIdCommand> {
        private readonly IGuestRepository guestRepository;
        private readonly IUnitOfWork unitOfWork;
        public GetGuestByIdHandler(IGuestRepository guestRepository, IUnitOfWork unitOfWork) {
            this.guestRepository = guestRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<GetGuestByIdCommand>> HandleAsync(GetGuestByIdCommand command) {
            Guest guest = await guestRepository.GetAsync(command.GuestId);
            await unitOfWork.SaveChangesASync();
            return command.AddResponse(guest.Id);
        }
    }
}
