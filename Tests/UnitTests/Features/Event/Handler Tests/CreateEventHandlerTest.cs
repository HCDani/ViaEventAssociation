using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Handlers.Event;
using ViaEventAssociation.Core.Application.Commands.Event;
using UnitTests.Features.Tools.Fakes;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Features.Event.Handler_Tests
{
    public class CreateEventHandlerTest
    {
        [Fact]
        public async Task CreateEventHandler_ValidInput_CreatesEvent() {
            // Arrange
            InMemEventRepoStub repo = new ();
            IUnitOfWork unitOfWork = new FUnitOfWork();
            ICommandHandler<CreateEventCommand> handler = new CreateEventHandler(repo, unitOfWork);

            CreateEventCommand command = CreateEventCommand.Create().payLoad;

            // Act
            Result<CreateEventCommand> result = await handler.HandleAsync(command);

            // Assert
            Assert.True(result.resultCode == 0);
            Assert.Single(repo.table);

            VEvent vEvent = repo.table.First().Value;
            Assert.Equal(result.payLoad.EventId, vEvent.Id);
        }
    }
}
