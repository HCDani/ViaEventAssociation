using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.LocationNS;

namespace IntegrationTests.RepositoryTests {

    [TestClass]
    public class LocationRepositoryTest {
        [ClassInitialize]
        public static void Initialize(TestContext _1) {
            using (var dbContext = GlobalUsings.CreateDbContext()) {
                GlobalUsings.InitializeDatabase(dbContext);
            }
        }

        [TestMethod]
        public void TestLocationRepository() {
            // Arrange
            using (var ctx = GlobalUsings.CreateDbContext()) {

                CommandDispatcher cd = new(ctx);
                // Act Create
                CreateLocationCommand clc = GlobalUsings.executeCommand(ctx,cd, CreateLocationCommand.Create("1111", "Test City", "Test Street", "5", "2025-05-20", "2025-05-21", "Test Name", "15"));
                Guid createdLocationId = clc.LocationId;
                GetLocationByIdCommand glbic = GlobalUsings.executeCommand(ctx, cd, GetLocationByIdCommand.Create(createdLocationId.ToString()));
                // Assert Create
                Assert.IsNotNull(glbic.Location);
                Assert.AreEqual(createdLocationId, glbic.Location.Id);
            }
        }
    }
}
