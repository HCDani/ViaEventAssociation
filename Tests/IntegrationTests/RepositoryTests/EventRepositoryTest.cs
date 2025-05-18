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
        public static void Initialize(TestContext _1) {
            using (var dbContext = GlobalUsings.CreateDbContext()) {
                GlobalUsings.InitializeDatabase(dbContext);
            }
        }

        [TestMethod]
        public void TestEventRepository() {
            // Arrange
            Guid createdEventId = Guid.Empty;
            using (var context = GlobalUsings.CreateDbContext()) {
                CommandDispatcher cd = new (context);
                // Act
                UpdateEventMaxnumberOfGuestsCommand cec = UpdateEventMaxnumberOfGuestsCommand.Create().payLoad;
                Task<Result<UpdateEventMaxnumberOfGuestsCommand>> res = cd.DispatchAsync(cec);
                res.Wait();
                Result<UpdateEventMaxnumberOfGuestsCommand> cecr = res.Result;
                Assert.IsTrue(cecr.IsSuccess());
                createdEventId = cecr.payLoad.EventId;
            }
            using (var assertcontext = GlobalUsings.CreateDbContext()) {
                CommandDispatcher cd = new (assertcontext);
                GetEventByIdCommand getEventByIdCommand = GetEventByIdCommand.Create(createdEventId.ToString()).payLoad;
                Task<Result<GetEventByIdCommand>> res = cd.DispatchAsync(getEventByIdCommand);
                res.Wait();
                Result<GetEventByIdCommand> ger = res.Result;
                // Assert
                Assert.IsTrue(ger.IsSuccess());
                Assert.IsNotNull(ger.payLoad.VEvent);
                Assert.AreEqual(ger.payLoad.VEvent.Id, createdEventId);
            }
        }
    }
}
