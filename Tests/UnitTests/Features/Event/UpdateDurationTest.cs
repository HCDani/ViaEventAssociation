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
    public class UpdateDurationTest
    {
        // ID 4 use case

        [Fact]
        public void UpdateDuration_WithValidData()
        {
            // Arrange S1
            EventId id = EventId.Create(Guid.NewGuid()).payLoad;
            Title title = Title.Create("Event Title").payLoad;
            Status status = Status.Create(Status.StatusEnum.Draft).payLoad;
            VEvent vEvent = VEvent.Create(id, title, status);
            // Act S1
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);

            // Assert S1
            Assert.Equal(new DateTime(2026, 10, 31, 9, 0, 0), vEvent.Duration.From);
            Assert.Equal(new DateTime(2026, 10, 31, 11, 11, 11), vEvent.Duration.To);

            // Arrange S2

            // Act S2
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 03, 20, 12, 0, 0), new DateTime(2026, 03, 20, 16, 30, 0)).payLoad);
            // Assert S2
            Assert.Equal(new DateTime(2026, 03, 20, 12, 0, 0), vEvent.Duration.From);
            Assert.Equal(new DateTime(2026, 03, 20, 16, 30, 0), vEvent.Duration.To);

            // Arrange S3
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(5).payLoad);
            vEvent.UpdateVisibility(Visibility.Create(Visibility.VisibilityEnum.Private).payLoad);
            vEvent.UpdateDescription(Description.Create("").payLoad);
            vEvent.UpdateLocationId(new LocationId(Guid.NewGuid()));
            Result<Status> resultStatus = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Ready).payLoad);

            Assert.Equal(0, resultStatus.resultCode);

            // Act S3
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 03, 21, 12, 0, 0), new DateTime(2026, 03, 21, 16, 30, 0)).payLoad);
            // Assert S3
            Assert.Equal(new DateTime(2026, 03, 21, 12, 0, 0), vEvent.Duration.From);
            Assert.Equal(new DateTime(2026, 03, 21, 16, 30, 0), vEvent.Duration.To);
            Assert.Equal(Status.StatusEnum.Draft, vEvent.Status.Value);

            // Arrange S4
            // Act S4
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 03, 20, 12, 0, 0), new DateTime(2026, 03, 20, 16, 30, 0)).payLoad);
            // Assert S4
            Assert.Equal(new DateTime(2026, 03, 20, 12, 0, 0), vEvent.Duration.From);
            Assert.Equal(new DateTime(2026, 03, 20, 16, 30, 0), vEvent.Duration.To);

            // Arrange S5
            // Act S5
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 03, 20, 19, 0, 0), new DateTime(2026, 03, 21, 01, 0, 0)).payLoad);
            // Assert S5
            Assert.Equal(new DateTime(2026, 03, 20, 19, 0, 0), vEvent.Duration.From);
            Assert.Equal(new DateTime(2026, 03, 21, 01, 0, 0), vEvent.Duration.To);

            // Arrange F1
            // Act F1
            Result<EventDuration> newDurationF1 = EventDuration.Create(new DateTime(2026, 08,26,19,0,0), new DateTime(2026,08,25,1,0,0));
            // Assert F1
            Assert.Equal(30, newDurationF1.resultCode);

            // Arrange F2
            // Act F2
            Result<EventDuration> newDurationF2 = EventDuration.Create(new DateTime(2026, 08, 26, 19, 0, 0), new DateTime(2026, 08, 26, 14, 0, 0));
            // Assert F2
            Assert.Equal(30, newDurationF2.resultCode);

            // Arrange F3
            // Act F3
            Result<EventDuration> newDurationF3 = EventDuration.Create(new DateTime(2026, 08, 26, 14, 0, 0), new DateTime(2026, 08, 26, 14, 50, 0));
            // Assert F3
            Assert.Equal(35, newDurationF3.resultCode);

            // Arrange F4
            // Act F4
            Result<EventDuration> newDurationF4 = EventDuration.Create(new DateTime(2026, 08, 25, 23, 30, 0), new DateTime(2026, 08, 26, 00, 15, 0));
            // Assert F4
            Assert.Equal(35, newDurationF4.resultCode);

            // Arrange F5
            // Act F5
            Result<EventDuration> newDurationF5 = EventDuration.Create(new DateTime(2026, 08, 25, 07, 59, 0), new DateTime(2026, 08, 26, 14, 0, 0));
            // Assert F5
            Assert.Equal(31, newDurationF5.resultCode);

            // Arrange F6
            // Act F6
            Result<EventDuration> newDurationF6 = EventDuration.Create(new DateTime(2026, 08, 25, 23, 0, 0), new DateTime(2026, 08, 26, 01, 01, 0));
            // Assert F6
            Assert.Equal(32, newDurationF6.resultCode);

            // Arrange F9
            // Act F9
            Result<EventDuration> newDurationF9 = EventDuration.Create(new DateTime(2026, 08, 30, 08, 0, 0), new DateTime(2026, 08, 30, 18, 01, 0));

            // Assert F9
            Assert.Equal(34, newDurationF9.resultCode);

            // Arrange F10
            // Act F10
            Result<EventDuration> newDurationF10 = EventDuration.Create(new DateTime(2024, 08, 30, 08, 0, 0), new DateTime(2024, 08, 30, 10, 01, 0));
            // Assert F10
            Assert.Equal(38, vEvent.UpdateDuration(newDurationF10.payLoad).resultCode);

            // Arrange F11
            // Act F11
            Result<EventDuration> newDurationF11 = EventDuration.Create(new DateTime(2026, 08, 26, 7, 59, 0), new DateTime(2026, 08, 26, 14, 50, 0));
            Result<EventDuration> newDurationF12 = EventDuration.Create(new DateTime(2026, 08, 26, 09, 00, 0), new DateTime(2026, 08, 27, 1, 20, 0));
            // Assert F11
            Assert.Equal(31, newDurationF11.resultCode);
            Assert.Equal(32, newDurationF12.resultCode);

            // This ended up in the end, because the cancelled status forbids any other changes.
            // Arrange F7
            Result<Status> resultStatusF7A = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Ready).payLoad);
            Assert.Equal(0, resultStatusF7A.resultCode);
            Result<Status> resultStatusF7B = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Active).payLoad);
            Assert.Equal(0, resultStatusF7B.resultCode);

            // Act F7
            Result<EventDuration> newDurationF7 = EventDuration.Create(new DateTime(2026, 08, 26, 20, 0, 0), new DateTime(2026, 08, 26, 23, 01, 0));

            // Assert F7
            Assert.Equal(36, vEvent.UpdateDuration(newDurationF7.payLoad).resultCode);
            Assert.Equal(new DateTime(2026, 03, 20, 19, 0, 0), vEvent.Duration.From);
            Assert.Equal(new DateTime(2026, 03, 21, 01, 0, 0), vEvent.Duration.To);

            // Arrange F8
            Result<Status> resultStatusF8 = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Cancelled).payLoad);
            Assert.Equal(0, resultStatusF8.resultCode);

            // Act F8
            Result<EventDuration> newDurationF8 = EventDuration.Create(new DateTime(2026, 08, 26, 20, 0, 0), new DateTime(2026, 08, 26, 23, 01, 0));

            // Assert F8
            Assert.Equal(37, vEvent.UpdateDuration(newDurationF8.payLoad).resultCode);
            Assert.Equal(new DateTime(2026, 03, 20, 19, 0, 0), vEvent.Duration.From);
            Assert.Equal(new DateTime(2026, 03, 21, 01, 0, 0), vEvent.Duration.To);

        }
    }
}
