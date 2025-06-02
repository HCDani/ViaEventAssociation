using IntegrationTests.RepositoryTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryApplication.QueryDispatching;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.Queries.Persistence;

namespace IntegrationTests.QueryTests {
    [TestClass]
    public class GetUnpublishedEventsTest {
        private const string Dbname = "../../UnpublishedEvents.db";

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
    }
}
