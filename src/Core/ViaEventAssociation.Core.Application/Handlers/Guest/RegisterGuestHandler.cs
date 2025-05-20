using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.GuestNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;

namespace ViaEventAssociation.Core.Application.Handlers.GuestNS {
    public class RegisterGuestHandler : ICommandHandler<RegisterGuestCommand> {
        private readonly IGuestRepository guestRepository;
        private readonly IUnitOfWork unitOfWork;

        public RegisterGuestHandler(IGuestRepository guestRepository, IUnitOfWork unitOfWork) {
            this.guestRepository = guestRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<RegisterGuestCommand>> HandleAsync(RegisterGuestCommand command) {
            Result<Guest> rGuest = Guest.RegisterGuest(Guid.NewGuid(), command.Name, command.Email, command.ProfilePictureUrl);
            if (rGuest.resultCode == 0) {
                await guestRepository.CreateAsync(rGuest.payLoad);
            }
            await unitOfWork.SaveChangesASync();
            return command.AddResponse(rGuest);
        }
    }
}
