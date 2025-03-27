using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event
{
    public class UpdateTitleTest
    {
        // ID2 use case.

        [Fact]
        public void UpdateTitle_WithValidData()
        {
            // Arrange S1
            EventId id = EventId.Create(Guid.NewGuid()).payLoad;
            Title title = Title.Create("Event Title").payLoad;
            Status status = Status.Create(Status.StatusEnum.Draft).payLoad;
            VEvent vEvent = VEvent.Create(id, title, status);

            // Act S1
            vEvent.UpdateTitle(Title.Create("Scary Movie Night!").payLoad);

            // Assert S1
            Assert.Equal("Scary Movie Night!", vEvent.Title.Value);
            Assert.Equal(Status.StatusEnum.Draft, vEvent.Status.Value);

            // Arrange S2
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(5).payLoad);
            vEvent.UpdateVisibility(Visibility.Create(Visibility.VisibilityEnum.Private).payLoad);
            vEvent.UpdateDescription(Description.Create("").payLoad);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31,9,0,0), new DateTime(2026, 10, 31,11,11,11)).payLoad);
            vEvent.UpdateLocationId(new LocationId(Guid.NewGuid()));
            Result<Status> resultStatus = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Ready).payLoad);

            Assert.Equal(0, resultStatus.resultCode);

            // Act S2
            vEvent.UpdateTitle(Title.Create("Graduation Gala").payLoad);

            //Assert S2
            Assert.Equal("Graduation Gala", vEvent.Title.Value);
            Assert.Equal(Status.StatusEnum.Draft, vEvent.Status.Value);

            // Arrange F1
            // Act F1
            Result<Title> newTitleF1 = Title.Create("");

            // Assert F1
            Assert.Equal(10, newTitleF1.resultCode);

            // Arrange F2
            // Act F2
            Result<Title> newTitleF2 = Title.Create("a");

            // Assert F2
            Assert.Equal(10, newTitleF2.resultCode);

            // Arrange F3
            // Act F3
            Result<Title> newTitleF3 = Title.Create("a123456789a123456789a123456789a123456789a123456789a123456789a123456789a123456789");

            // Assert F3
            Assert.Equal(10, newTitleF3.resultCode);

            // Arrange F4
            // Act F4
            Result<Title> newTitleF4 = Title.Create(null);

            // Assert F4
            Assert.Equal(10, newTitleF4.resultCode);
            Assert.Equal(10, vEvent.UpdateTitle(null).resultCode);

            // Arrange F5
            Result<Status> resultStatusF5 = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Ready).payLoad);
            Assert.Equal(0, resultStatusF5.resultCode);
            Result<Status> resultStatusF5A = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Active).payLoad);
            Assert.Equal(0, resultStatusF5A.resultCode);

            // Act F5
            Result<Title> newTitleF5 = Title.Create("VIA Hackathon");
            
            // Assert F5
            Assert.Equal(11, vEvent.UpdateTitle(newTitleF5.payLoad).resultCode);
            Assert.Equal("Graduation Gala",vEvent.Title.Value);

            // Arrange F6
            Result<Status> resultStatusF6 = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Cancelled).payLoad);
            Assert.Equal(0, resultStatusF6.resultCode);

            // Act F6
            Result<Title> newTitleF6 = Title.Create("VIA Hackathon");

            // Assert F6
            Assert.Equal(12, vEvent.UpdateTitle(newTitleF6.payLoad).resultCode);
            Assert.Equal("Graduation Gala", vEvent.Title.Value);
        }
    }
}
