using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.Command_Tests
{
    public class CreateEventCommandTest
    {
        [Fact]
        public void CreateEventCommand_ValidInput_CreatesEvent() {
            // Arrange
            Result<CreateEventCommand> result = CreateEventCommand.Create();
            CreateEventCommand command = result.payLoad;

            // Act
            // Assert
            Assert.NotNull(result);
            Assert.True(result.resultCode == 0);
            Assert.True(command.EventId == Guid.Empty);
        }
    }
}
