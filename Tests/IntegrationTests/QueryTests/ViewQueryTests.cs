using IntegrationTests.RepositoryTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
                Assert.IsTrue(result.Events.AsQueryable().Count(ei => ei.CurrentNumberOfGuests > 0) > 0,"At least one event have to have more then 0 active participants");



            }
        }

    }
}
