using IntegrationTests.RepositoryTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnitTests.Features.Tools.Fakes;
using ViaEventAssociation.Core.Tools.SystemTime;
using ViaEventAssociation.Core.QueryApplication.QueryDispatching;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.Queries.Persistence;

namespace IntegrationTests.QueryTests {
    [TestClass]
    public class ViewQueryTests {
        private const string Dbname = "../../TestViewQueries.db";

        [ClassInitialize]
        public static void Initialize(TestContext _1) {
            using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                JSONLoader.SeedDatabaseFromJson(ctx).Wait();
            }
        }

        [TestMethod]
        public async Task TestViewQueries() {
            // Arrange
            using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                IQueryDispatcher queryDispatcher = new QueryDispatcher(ctx);

                // Act
                GuestEvents.Query query = new GuestEvents.Query(Guid.Parse("503457fb-dd03-40f6-8e89-79aee51a8736"));
                GuestEvents.Answer result = await queryDispatcher.DispatchAsync<GuestEvents.Query, GuestEvents.Answer>(query);
                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Events.Count > 0, "Expected at least one event for the guest.");
                Assert.IsTrue(result.Events.AsQueryable().Count(ei => ei.CurrentNumberOfGuests > 0) > 0,"At least one event have to have more than 0 active participants");
            }
        }
        [TestMethod]
        public async Task TestGetUnpublishedEvents() {
            // Arrange
            using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                IQueryDispatcher queryDispatcher = new QueryDispatcher(ctx);
                // Act
                UnpublishedEvents.Query query = new UnpublishedEvents.Query();
                UnpublishedEvents.Answer result = await queryDispatcher.DispatchAsync<UnpublishedEvents.Query, UnpublishedEvents.Answer>(query);
                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.uEvents.Count > 0, "Expected at least one unpublished event.");
                Assert.IsTrue(result.uEvents.FindAll(e => e.Status == 0).Count() > 0);
                Assert.IsTrue(result.uEvents.FindAll(e => e.Status == 1).Count() > 0);
                Assert.IsTrue(result.uEvents.FindAll(e => e.Status == 2).Count() > 0);
            }
        }
        [TestMethod]
        public async Task TestGetUpcomingEvents() {
            try {
                // Arrange
                FakeSystemTime.FakeSystemTimeMutex.WaitOne();
                FakeSystemTime.SetSystemTime(new DateTime(2024, 01, 01, 9, 0, 0));
                using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                    IQueryDispatcher queryDispatcher = new QueryDispatcher(ctx);
                    // Act
                    UpcomingEvents.Query query = new UpcomingEvents.Query(Guid.Parse("503457fb-dd03-40f6-8e89-79aee51a8736"));
                    UpcomingEvents.Answer result = await queryDispatcher.DispatchAsync<UpcomingEvents.Query, UpcomingEvents.Answer>(query);
                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsTrue(result.UpEvents.Count > 0, "Expected at least one upcoming event.");
                    Assert.IsTrue(result.UpEvents.AsQueryable().Count(ei => ei.CurrentNumberOfGuests > 0) > 0, "At least one event have to have more than 0 active participants");
                }
            } finally {
                FakeSystemTime.FakeSystemTimeMutex.ReleaseMutex();
            }
        }
        [TestMethod]
        public async Task TestGetGuestInfo() {
            try {
                // Arrange
                FakeSystemTime.FakeSystemTimeMutex.WaitOne();
                FakeSystemTime.SetSystemTime(new DateTime(2024, 01, 01, 9, 0, 0));
                using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                    IQueryDispatcher queryDispatcher = new QueryDispatcher(ctx);
                    // Act
                    GuestInfo.Query query = new GuestInfo.Query(Guid.Parse("503457fb-dd03-40f6-8e89-79aee51a8736"));
                    GuestInfo.Answer result = await queryDispatcher.DispatchAsync<GuestInfo.Query, GuestInfo.Answer>(query);
                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual("Adhvik", result.FName, "Expected guest fname to match.");
                    Assert.AreEqual("Alvarez", result.LName, "Expected guest lname to match.");
                    Assert.AreEqual(result.UpComingEventCount, 5, "Expected 5 upcoming event.");
                }
            } finally {
                FakeSystemTime.FakeSystemTimeMutex.ReleaseMutex();
            }
        }
        [TestMethod]
        public async Task TestGetEventInfo() {
            // Arrange
            using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                IQueryDispatcher queryDispatcher = new QueryDispatcher(ctx);
                // Act
                EventInfo.Query query = new EventInfo.Query(Guid.Parse("40ed2fd9-2240-4791-895f-b9da1a1f64e4"));
                EventInfo.Answer result = await queryDispatcher.DispatchAsync<EventInfo.Query, EventInfo.Answer>(query);
                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual("Friday Bar", result.Title, "Expected event name to match.");
                Assert.AreEqual("Come for the cheap beer and great company.", result.Description, "Expected event desc to match.");
                Assert.AreEqual(50, result.MaxNumberOfGuests, "Expected max capacity to match.");
                Assert.AreEqual(27, result.CurrentNumberOfGuests, "Expected current numbers of guests to match");
                Assert.AreEqual("Guest Canteen", result.LocationName, "Expected the location name to match");
                Assert.AreEqual(1, result.Visibility, "Expected visibility to match");
                Assert.AreEqual(new DateTime(2024, 03, 01, 15, 00, 00), result.From, "Expected the from time to match");
                Assert.AreEqual(new DateTime(2024, 03, 01, 21, 00, 00), result.To, "Expected the to time to match");
            }
        }
    }
}
