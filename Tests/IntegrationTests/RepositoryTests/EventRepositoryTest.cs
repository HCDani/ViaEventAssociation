using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Infrastructure.Persistence.Repositories;

namespace IntegrationTests.RepositoryTests {
    [TestClass]
    public class EventRepositoryTest {

        [ClassInitialize]
        public void Initialize() {
            using (var dbContext = GlobalUsings.CreateDbContext()) {
                GlobalUsings.InitializeDatabase(dbContext);
            }
        }

        [TestMethod]
        public void TestEventRepository() {
            // Arrange
            using (var context = GlobalUsings.CreateDbContext()) {
                CommandDispatcher cd = new CommandDispatcher(context);
                // Act
                CreateEventCommand cec = CreateEventCommand.Create().payLoad;
                Task<Result<CreateEventCommand>> res = cd.DispatchAsync(cec);
                res.Wait();
                Result<CreateEventCommand> cecr = res.Result;
                Assert.IsTrue(cecr.IsSuccess());

                var eventRepository = new EventRepository(context);
                var retrievedEvent = eventRepository.GetAsync(cecr.payLoad.EventId);
                // Assert
                Assert.IsNotNull(retrievedEvent);
                Assert.Equals(cecr.payLoad.EventId, retrievedEvent.Id);
            }
        }
    }
}
