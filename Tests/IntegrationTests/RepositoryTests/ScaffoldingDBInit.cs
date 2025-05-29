using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Application.Commands.EventGuestParticipation;
using ViaEventAssociation.Core.Application.Commands.GuestNS;
using ViaEventAssociation.Core.Application.Commands.LocationNS;

namespace IntegrationTests.RepositoryTests {
    [TestClass]
    public class ScaffoldingDBInit {
        private const string Dbname = "../../../ScaffoldingDBInit.db";
        [TestMethod]
        public void TestDBInit() {
            // Arrange
            using (var ctx = GlobalUsings.CreateDbContext(Dbname)) {
                GlobalUsings.InitializeDatabase(ctx);
                CommandDispatcher cd = new(ctx);
                CreateLocationCommand clc = GlobalUsings.executeCommand(ctx, cd, CreateLocationCommand.Create("1111", "Test City", "Test Street", "5", "2025-05-20", "2025-05-21", "Test Name", "15"));
                // Act Create
                CreateEventCommand cecr = GlobalUsings.executeCommand(ctx, cd, CreateEventCommand.Create());
                Guid createdEventId = cecr.EventId;

                GetEventByIdCommand ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));
                // Assert Create
                Assert.IsNotNull(ger.VEvent);
                Assert.AreEqual(createdEventId, ger.VEvent.Id);

                // Act UpdateEventDescription
                UpdateEventDescriptionCommand uedc = GlobalUsings.executeCommand(ctx, cd, UpdateEventDescriptionCommand.Create(createdEventId.ToString(), "New Description"));
                ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));

                // Assert UpdateEventDescription
                Assert.IsNotNull(ger.VEvent);
                Assert.AreEqual("New Description", ger.VEvent.Description.Value);

                // Act UpdateEventDuration
                UpdateEventDurationCommand ueduc = GlobalUsings.executeCommand(ctx, cd, UpdateEventDurationCommand.Create(createdEventId.ToString(), DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(2)));
                ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));

                // Assert UpdateEventDuration
                Assert.IsNotNull(ger.VEvent);
                Assert.IsNotNull(ger.VEvent.Duration.From);
                Assert.IsNotNull(ger.VEvent.Duration.To);

                // Act UpdateEventLocation
                UpdateEventLocationCommand uelc = GlobalUsings.executeCommand(ctx, cd, UpdateEventLocationCommand.Create(createdEventId.ToString(), clc.LocationId.ToString()));
                ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));

                // Assert UpdateEventLocation
                Assert.IsNotNull(ger.VEvent);
                Assert.IsNotNull(ger.VEvent.Location);

                // Act UpdateEventMaxNumberOfGuests
                UpdateEventMaxNumberOfGuestsCommand uemngc = GlobalUsings.executeCommand(ctx, cd, UpdateEventMaxNumberOfGuestsCommand.Create(createdEventId.ToString(), "10"));
                ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));

                // Assert UpdateEventMaxNumberOfGuests
                Assert.IsNotNull(ger.VEvent);
                Assert.AreEqual(10, ger.VEvent.MaxNumberOfGuests.Value);

                // Act UpdateEventTitle
                UpdateEventTitleCommand uetc = GlobalUsings.executeCommand(ctx, cd, UpdateEventTitleCommand.Create(createdEventId.ToString(), "New Title"));
                ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));

                // Assert UpdateEventTitle
                Assert.IsNotNull(ger.VEvent);
                Assert.AreEqual("New Title", ger.VEvent.Title.Value);

                // Act UpdateEventVisibility
                UpdateEventVisibilityCommand uevc = GlobalUsings.executeCommand(ctx, cd, UpdateEventVisibilityCommand.Create(createdEventId.ToString(), "Public"));
                ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));

                // Assert UpdateEventVisibility
                Assert.IsNotNull(ger.VEvent);
                Assert.AreEqual("Public", ger.VEvent.Visibility.ToString());

                // Act UpdateEventStatus
                UpdateEventStatusCommand uesc = GlobalUsings.executeCommand(ctx, cd, UpdateEventStatusCommand.Create(createdEventId.ToString(), "Ready"));
                ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));

                // Assert UpdateEventStatus
                Assert.IsNotNull(ger.VEvent);
                Assert.AreEqual("Ready", ger.VEvent.Status.ToString());

                // Act UpdateEventStatus
                UpdateEventStatusCommand uesca = GlobalUsings.executeCommand(ctx, cd, UpdateEventStatusCommand.Create(createdEventId.ToString(), "Active"));
                ger = GlobalUsings.executeCommand(ctx, cd, GetEventByIdCommand.Create(createdEventId.ToString()));

                // Assert UpdateEventStatus
                Assert.IsNotNull(ger.VEvent);
                Assert.AreEqual("Active", ger.VEvent.Status.ToString());

                // Assert RegisterGuest
                RegisterGuestCommand rgc = GlobalUsings.executeCommand(ctx, cd, RegisterGuestCommand.Create("email@via.dk", "FirstName", "LastName", "https://example.com/profile.jpg"));
                Guid createdGuestId = rgc.GuestId;

                GetGuestByIdCommand ggbic = GlobalUsings.executeCommand(ctx, cd, GetGuestByIdCommand.Create(createdGuestId.ToString()));
                // Assert Create
                Assert.IsNotNull(ggbic.Guest);
                Assert.AreEqual(createdGuestId, ggbic.Guest.Id);

                // Act Create Event Participation
                CreateEventParticipationCommand cepc = GlobalUsings.executeCommand(ctx, cd, CreateEventParticipationCommand.Create(createdEventId.ToString(), createdGuestId.ToString(), "Participating"));
                //Assert Create Event Participation
                Assert.AreNotEqual(cepc.ParticipationId, Guid.Empty);
            }
        }
    }
}
