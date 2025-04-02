using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;

namespace UnitTests.Features.Event
{
    public class EventCreationTest
    {
        // ID1 use case.

        [Fact]
        public void CreateEvent_WithValidData()
        {
            // Arrange S1
            // Act S1
            VEvent vEvent = VEvent.Create(Guid.NewGuid(), EventStatus.Draft);
            // Assert S1
            Assert.Equal(EventStatus.Draft, vEvent.Status);
            Assert.Equal(5, vEvent.MaxNumberOfGuests.Value);

            // Arrange S2
            // Act S2
            // Assert S2
            Assert.Equal(Title.Default.Value, vEvent.Title.Value);

            // Arrange S3
            // Act S3
            // Assert S3
            Assert.Equal(Description.Default.Value, vEvent.Description.Value);

            // Arrange S4
            // Act S4
            // Assert S4
            Assert.Equal(Visibility.Private, vEvent.Visibility);

        }
    }
}
