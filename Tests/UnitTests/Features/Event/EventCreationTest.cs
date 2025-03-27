using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.Values;

namespace UnitTests.Features.Event
{
    public class EventCreationTest
    {
        // ID1 use case.

        [Fact]
        public void CreateEvent_WithValidData()
        {
            EventId id = EventId.Create(Guid.NewGuid()).payLoad;
            Title title = Title.Create("Event Title").payLoad;
            Status status = Status.Create(Status.StatusEnum.Draft).payLoad;
            // Arrange
            VEvent vEvent = VEvent.Create(id, title, status);

            // Act
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(5).payLoad);
            vEvent.UpdateVisibility(Visibility.Create(Visibility.VisibilityEnum.Private).payLoad);
            vEvent.UpdateDescription(Description.Create("").payLoad);

            // Assert
            Assert.Equal(Status.StatusEnum.Draft, vEvent.Status.Value);
            Assert.Equal(5, vEvent.MaxNumberOfGuests.Value);
            Assert.Equal(Visibility.VisibilityEnum.Private, vEvent.Visibility.Value);
            Assert.Equal("", vEvent.Description.Value);
        }
    }
}
