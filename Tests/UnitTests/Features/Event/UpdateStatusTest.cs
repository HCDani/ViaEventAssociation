using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event
{
   public class UpdateStatusTest
    {
        // ID 8 use case.
        [Fact]
        public void UpdateStatusTest_Ready() {
            // Arrange S1
            Title title = Title.Create("Event Title").payLoad;
            VEvent vEvent = VEvent.Create(Guid.NewGuid(), Status.Draft);
            vEvent.UpdateTitle(title);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            // Act S1
            Result<Status> resultStatusS1 = vEvent.UpdateStatus(Status.Ready);
            // Assert S1
            Assert.Equal(0, resultStatusS1.resultCode);
            Assert.Equal(Status.Ready, vEvent.Status);
            // Arrange F1
            vEvent = VEvent.Create(Guid.NewGuid(), Status.Draft);
            vEvent.UpdateTitle(title);
            // Act F1
            Result<Status> resultStatusF1 = vEvent.UpdateStatus(Status.Ready);
            // Assert F1
            Assert.Equal(4, resultStatusF1.resultCode);
            Assert.Equal(Status.Draft, vEvent.Status);
            // Arrange F2
            Result<Status> resultStatusF2A = vEvent.UpdateStatus(Status.Cancelled);
            // Act F2
            Result<Status> resultStatusF2B = vEvent.UpdateStatus(Status.Ready);
            // Assert F2
            Assert.Equal(2, resultStatusF2B.resultCode);

            // Arrange F3 This only works if the current time is after 8 am and before midnight by 2 seconds.
            if(DateTime.Now.Hour > 8 || DateTime.Now.Hour < 23) {
                vEvent = VEvent.Create(Guid.NewGuid(), Status.Draft);
                vEvent.UpdateTitle(title);
                Result<EventDuration> newDurationF10 = EventDuration.Create(DateTime.Now.AddSeconds(1), DateTime.Now.AddHours(1).AddSeconds(3));
                // Act F3
                vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);

                // Assert F3
                Assert.Equal(0, vEvent.UpdateDuration(newDurationF10.payLoad).resultCode);
                Thread.Sleep(1500);
                Result<Status> resultStatusF3 = vEvent.UpdateStatus(Status.Ready);
                Assert.Equal(38, resultStatusF3.resultCode);
            }

            // Arrange F4
            vEvent = VEvent.Create(Guid.NewGuid(), Status.Draft);
            // Act F4
            Result<Status> resultStatusF4 = vEvent.UpdateStatus(Status.Ready);
            // Assert F4
            Assert.Equal(3, resultStatusF4.resultCode);
        }

        // ID 9 use case.
        [Fact]
        public void UpdateStatusTest_Activate()
        {
            // Arrange S1
            Title title = Title.Create("Event Title").payLoad;
            VEvent vEvent = VEvent.Create(Guid.NewGuid(), Status.Draft);
            vEvent.UpdateTitle(title);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            // Act S1
            Result<Status> resultStatusS1A = vEvent.UpdateStatus(Status.Ready);
            Result<Status> resultStatusS1B = vEvent.UpdateStatus(Status.Active);
            // Assert S1
            Assert.Equal(0, resultStatusS1A.resultCode);
            Assert.Equal(0, resultStatusS1B.resultCode);

            // Arrange S2
            vEvent = VEvent.Create(Guid.NewGuid(), Status.Draft);
            vEvent.UpdateTitle(title);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            Result<Status> resultStatusS2A = vEvent.UpdateStatus(Status.Ready);
            Assert.Equal(0, resultStatusS2A.resultCode);
            // Act S2
            Result<Status> resultStatusS2B = vEvent.UpdateStatus(Status.Active);
            // Assert S2
            Assert.Equal(0, resultStatusS2B.resultCode);

            // Arrange S3
            // Act S3
            Result<Status> resultStatusS3 = vEvent.UpdateStatus(Status.Active);
            // Assert S3
            Assert.Equal(0, resultStatusS3.resultCode);

            // Arrange F1
            vEvent = VEvent.Create(Guid.NewGuid(), Status.Draft);
            vEvent.UpdateDescription(null);
            // Act F1
            Result<Status> resultStatusF1 = vEvent.UpdateStatus(Status.Ready);
            // Assert F1
            Assert.Equal(3, resultStatusF1.resultCode);
            // Act F1
            vEvent.UpdateTitle(title);
            resultStatusF1 = vEvent.UpdateStatus(Status.Ready);
            // Assert F1
            Assert.Equal(4, resultStatusF1.resultCode);
            // Act F1
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            resultStatusF1 = vEvent.UpdateStatus(Status.Ready);
            // Assert F1
            Assert.Equal(5, resultStatusF1.resultCode);
            // Act F1
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            resultStatusF1 = vEvent.UpdateStatus(Status.Ready);
            // Assert F1
            Assert.Equal(6, resultStatusF1.resultCode);

            // Arrange F2
            resultStatusF1 = vEvent.UpdateStatus(Status.Cancelled);
            Assert.Equal(0, resultStatusF1.resultCode);
            // Act F2
            resultStatusF1 = vEvent.UpdateStatus(Status.Active);
            // Assert F2
            Assert.Equal(2, resultStatusF1.resultCode);
        }
    }
}
