using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Application.Commands.LocationNS;
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
            using (var context = GlobalUsings.CreateDbContext()) {
                using (var assertcontext = GlobalUsings.CreateDbContext()) {
                    CommandDispatcher cd = new (context);
                    CommandDispatcher acd = new(assertcontext);
                    CreateLocationCommand clc = GlobalUsings.executeCommand(cd, CreateLocationCommand.Create("1111", "Test City","Test Street", "5", "2025-05-20", "2025-05-21", "Test Name", "15"));
                    // Act Create
                    CreateEventCommand cecr = GlobalUsings.executeCommand(cd, CreateEventCommand.Create());
                    Guid createdEventId = cecr.EventId;

                    GetEventByIdCommand ger = GlobalUsings.executeCommand(acd, GetEventByIdCommand.Create(createdEventId.ToString()));
                    // Assert Create
                    Assert.IsNotNull(ger.VEvent);
                    Assert.AreEqual(createdEventId,ger.VEvent.Id);

                    // Act UpdateEventDescription
                    UpdateEventDescriptionCommand uedc = GlobalUsings.executeCommand(cd, UpdateEventDescriptionCommand.Create(createdEventId.ToString(), "New Description"));
                    ger = GlobalUsings.executeCommand(acd, GetEventByIdCommand.Create(createdEventId.ToString()));

                    // Assert UpdateEventDescription
                    Assert.IsNotNull(ger.VEvent);
                    Assert.AreEqual("New Description", ger.VEvent.Description.Value);

                    // Act UpdateEventDuration
                    UpdateEventDurationCommand ueduc = GlobalUsings.executeCommand(cd, UpdateEventDurationCommand.Create(createdEventId.ToString(), DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(2)));
                    ger = GlobalUsings.executeCommand(acd, GetEventByIdCommand.Create(createdEventId.ToString()));

                    // Assert UpdateEventDuration
                    Assert.IsNotNull(ger.VEvent);
                    Assert.IsNotNull(ger.VEvent.Duration.From);
                    Assert.IsNotNull(ger.VEvent.Duration.To);

                    // Act UpdateEventLocation
                    UpdateEventLocationCommand uelc = GlobalUsings.executeCommand(cd, UpdateEventLocationCommand.Create(createdEventId.ToString(), clc.LocationId.ToString()));
                    ger = GlobalUsings.executeCommand(acd, GetEventByIdCommand.Create(createdEventId.ToString()));

                    // Assert UpdateEventLocation
                    Assert.IsNotNull(ger.VEvent);
                    Assert.IsNotNull(ger.VEvent.Location);
                }
            }
        }
    }
}
