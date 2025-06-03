using IntegrationTests.RepositoryTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnitTests.Features.Tools.Fakes;
using ViaEventAssociation.Core.Domain.Services;
using ViaEventAssociation.Core.QueryApplication.QueryDispatching;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.Queries.Persistence;

namespace IntegrationTests.QueryTests {
    [TestClass]
    public class ViewQueryTests {
        private const string Dbname = "../../TestViewQueries.db";

        [TestMethod]
        public async Task TestViewQueries() {
            // Arrange
            using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                await JSONLoader.SeedDatabaseFromJson(ctx);
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
                await JSONLoader.SeedDatabaseFromJson(ctx);
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
            // Arrange
            FakeSystemTime fakeSystemTime = new FakeSystemTime();
            fakeSystemTime.DateTime = new DateTime(2024, 01, 01, 9, 0, 0);
            SystemTimeHolder.SetSystemTime(fakeSystemTime);
            using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                await JSONLoader.SeedDatabaseFromJson(ctx);
                IQueryDispatcher queryDispatcher = new QueryDispatcher(ctx);
                // Act
                UpcomingEvents.Query query = new UpcomingEvents.Query(Guid.Parse("503457fb-dd03-40f6-8e89-79aee51a8736"));
                UpcomingEvents.Answer result = await queryDispatcher.DispatchAsync<UpcomingEvents.Query, UpcomingEvents.Answer>(query);
                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.UpEvents.Count > 0, "Expected at least one upcoming event.");
                Assert.IsTrue(result.UpEvents.AsQueryable().Count(ei => ei.CurrentNumberOfGuests > 0) > 0, "At least one event have to have more than 0 active participants");
            }
        }

    }
}
