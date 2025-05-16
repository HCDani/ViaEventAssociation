using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Infrastructure.Persistence.Repositories;

namespace IntegrationTests.RepositoryTests {
    [TestClass]
    public class EventRepositoryTest {

        [TestMethod]
        public void TestEventRepository() {
            // Arrange
            using (var context = GlobalUsings.CreateDbContext()) {
                var eventRepository = new EventRepository(context);
                var eventId = Guid.NewGuid();
                var vEvent = new VEvent(eventId);
                context.Events.Add(vEvent);
                context.SaveChanges();
                // Act
                var retrievedEvent = eventRepository.GetById(eventId);
                // Assert
                Assert.IsNotNull(retrievedEvent);
                Assert.AreEqual(eventId, retrievedEvent.Id);
            }
        }
    }
}
