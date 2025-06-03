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
using UnitTests.Features.Tools.Fakes;
using ViaEventAssociation.Core.Tools.SystemTime;

namespace UnitTests.Features.Event
{
   public class UpdateStatusTest
    {
        // ID 8 use case.
        [Fact]
        public void UpdateStatusTest_Ready() {
            // Arrange S1
            FakeSystemTime fakeSystemTime = new FakeSystemTime(new DateTime(2026, 05, 27, 9, 0, 0));
            SystemTimeHolder.SetSystemTime(fakeSystemTime);
            Title title = Title.Create("Event Title").payLoad;
            VEvent vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(title);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            // Act S1
            Result<EventStatus> resultStatusS1 = vEvent.UpdateStatus(EventStatus.Ready);
            // Assert S1
            Assert.Equal(0, resultStatusS1.resultCode);
            Assert.Equal(EventStatus.Ready, vEvent.Status);
            // Arrange F1
            vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(title);
            // Act F1
            Result<EventStatus> resultStatusF1 = vEvent.UpdateStatus(EventStatus.Ready);
            // Assert F1
            Assert.Equal(4, resultStatusF1.resultCode);
            Assert.Equal(EventStatus.Draft, vEvent.Status);
            // Arrange F2
            Result<EventStatus> resultStatusF2A = vEvent.UpdateStatus(EventStatus.Cancelled);
            // Act F2
            Result<EventStatus> resultStatusF2B = vEvent.UpdateStatus(EventStatus.Ready);
            // Assert F2
            Assert.Equal(2, resultStatusF2B.resultCode);

            // Arrange F3
            vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateTitle(title);
            Result<EventDuration> newDurationF10 = EventDuration.Create(new DateTime (2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 10, 0, 0));
            Assert.Equal(0, vEvent.UpdateDuration(newDurationF10.payLoad).resultCode);
            fakeSystemTime.DateTime = new DateTime(2026, 10, 31, 9, 0, 1);
            // Act F3
            Result<EventStatus> resultStatusF3 = vEvent.UpdateStatus(EventStatus.Ready);

            // Assert F3
            Assert.Equal(38, resultStatusF3.resultCode);


            // Arrange F4
            vEvent = VEvent.Create(Guid.NewGuid());
            // Act F4
            Result<EventStatus> resultStatusF4 = vEvent.UpdateStatus(EventStatus.Ready);
            // Assert F4
            Assert.Equal(3, resultStatusF4.resultCode);
        }

        // ID 9 use case.
        [Fact]
        public void UpdateStatusTest_Activate()
        {
            // Arrange S1
            Title title = Title.Create("Event Title").payLoad;
            VEvent vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(title);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            // Act S1
            Result<EventStatus> resultStatusS1A = vEvent.UpdateStatus(EventStatus.Ready);
            Result<EventStatus> resultStatusS1B = vEvent.UpdateStatus(EventStatus.Active);
            // Assert S1
            Assert.Equal(0, resultStatusS1A.resultCode);
            Assert.Equal(0, resultStatusS1B.resultCode);

            // Arrange S2
            vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(title);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            Result<EventStatus> resultStatusS2A = vEvent.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, resultStatusS2A.resultCode);
            // Act S2
            Result<EventStatus> resultStatusS2B = vEvent.UpdateStatus(EventStatus.Active);
            // Assert S2
            Assert.Equal(0, resultStatusS2B.resultCode);

            // Arrange S3
            // Act S3
            Result<EventStatus> resultStatusS3 = vEvent.UpdateStatus(EventStatus.Active);
            // Assert S3
            Assert.Equal(0, resultStatusS3.resultCode);

            // Arrange F1
            vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateDescription(null);
            // Act F1
            Result<EventStatus> resultStatusF1 = vEvent.UpdateStatus(EventStatus.Ready);
            // Assert F1
            Assert.Equal(3, resultStatusF1.resultCode);
            // Act F1
            vEvent.UpdateTitle(title);
            resultStatusF1 = vEvent.UpdateStatus(EventStatus.Ready);
            // Assert F1
            Assert.Equal(4, resultStatusF1.resultCode);
            // Act F1
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            resultStatusF1 = vEvent.UpdateStatus(EventStatus.Ready);
            // Assert F1
            Assert.Equal(5, resultStatusF1.resultCode);
            // Act F1
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            resultStatusF1 = vEvent.UpdateStatus(EventStatus.Ready);
            // Assert F1
            Assert.Equal(6, resultStatusF1.resultCode);

            // Arrange F2
            resultStatusF1 = vEvent.UpdateStatus(EventStatus.Cancelled);
            Assert.Equal(0, resultStatusF1.resultCode);
            // Act F2
            resultStatusF1 = vEvent.UpdateStatus(EventStatus.Active);
            // Assert F2
            Assert.Equal(2, resultStatusF1.resultCode);
        }
    }
}
