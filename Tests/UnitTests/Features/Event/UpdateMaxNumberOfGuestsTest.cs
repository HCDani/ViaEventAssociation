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
   public class UpdateMaxNumberOfGuestsTest
    {
        // ID 7 use case.
        [Fact]
        public void UpdateMaxNumberOfGuests_WithValidData()
        {
            // Arrange S1
            EventId id = EventId.Create(Guid.NewGuid()).payLoad;
            Title title = Title.Create("Event Title").payLoad;
            VEvent vEvent = VEvent.Create(id, Status.Draft);
            vEvent.UpdateTitle(title);
            // Act S1
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(5).payLoad);
            // Assert S1
            Assert.Equal(5, vEvent.MaxNumberOfGuests.Value);
            // Arrange S2
            // Act S2
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            // Assert S2
            Assert.Equal(10, vEvent.MaxNumberOfGuests.Value);
            // Arrange S3
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocationId(new LocationId(Guid.NewGuid()));
            Result<Status> resultStatusS3A = vEvent.UpdateStatus(Status.Ready);
            Assert.Equal(0, resultStatusS3A.resultCode);
            Result<Status> resultStatusS3B = vEvent.UpdateStatus(Status.Active);
            Assert.Equal(0, resultStatusS3B.resultCode);
            // Act S3
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(15).payLoad);
            // Assert S3
            Assert.Equal(15, vEvent.MaxNumberOfGuests.Value);
            // Arrange F1
            // Act F1
            Result<MaxNumberOfGuests> resultMaxNumberOfGuestsF1 = vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(14).payLoad);
            // Assert F1
            Assert.Equal(51, resultMaxNumberOfGuestsF1.resultCode);
            // Arrange F2
            Result<Status> resultStatusF2 = vEvent.UpdateStatus(Status.Cancelled);
            Assert.Equal(0, resultStatusF2.resultCode);
            // Act F2
            Result<MaxNumberOfGuests> resultMaxNumberOfGuestsF2 = vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(20).payLoad);
            // Assert F2
            Assert.Equal(52, resultMaxNumberOfGuestsF2.resultCode);

            //TODO: F3

            // Arrange F4
            // Act F4
            Result<MaxNumberOfGuests> resultMaxNumberOfGuestsF4 = MaxNumberOfGuests.Create(0);
            // Assert F4
            Assert.Equal(53, resultMaxNumberOfGuestsF4.resultCode);

            // Arrange F5
            // Act F5
            Result<MaxNumberOfGuests> resultMaxNumberOfGuestsF5 = MaxNumberOfGuests.Create(51);
            // Assert F5
            Assert.Equal(54, resultMaxNumberOfGuestsF5.resultCode);
        }
    }
}
