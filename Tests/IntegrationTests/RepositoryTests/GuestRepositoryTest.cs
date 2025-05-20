using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Application.Commands.GuestNS;

namespace IntegrationTests.RepositoryTests {

    [TestClass]
    public class GuestRepositoryTest {
        [ClassInitialize]
        public static void Initialize(TestContext _1) {
            using (var dbContext = GlobalUsings.CreateDbContext()) {
                GlobalUsings.InitializeDatabase(dbContext);
            }
        }

        [TestMethod]
        public void TestGuestRepository() {
            using (var ctx = GlobalUsings.CreateDbContext()) {
                CommandDispatcher cd = new(ctx);
                // Act Create
                RegisterGuestCommand rgc = GlobalUsings.executeCommand(ctx,cd, RegisterGuestCommand.Create("email@via.dk", "FirstName", "LastName", "https://example.com/profile.jpg"));
                Guid createdGuestId = rgc.GuestId;

                GetGuestByIdCommand ggbic = GlobalUsings.executeCommand(ctx,cd, GetGuestByIdCommand.Create(createdGuestId.ToString()));
                // Assert Create
                Assert.IsNotNull(ggbic.Guest);
                Assert.AreEqual(createdGuestId, ggbic.Guest.Id);
            }
        }
    }
}
