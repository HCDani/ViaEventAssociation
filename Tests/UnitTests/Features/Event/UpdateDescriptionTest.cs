using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event
{
    public class UpdateDescriptionTest
    {
        // ID 3 use case.

        [Fact]
        public void UpdateDescription_WithValidData()
        {
            // Arrange S1
            EventId id = EventId.Create(Guid.NewGuid()).payLoad;
            Title title = Title.Create("Event Title").payLoad;
            Status status = Status.Create(Status.StatusEnum.Draft).payLoad;
            VEvent vEvent = VEvent.Create(id, title, status);

            // Act S1
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);

            // Assert S1
            Assert.Equal("Nullam tempor lacus nisl, eget tempus", vEvent.Description.Value);

            // Arrange S2
            // Act S2
            vEvent.UpdateDescription(Description.Create("").payLoad);

            // Assert S2
            Assert.Equal("", vEvent.Description.Value);

            // Arrange S3
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(5).payLoad);
            vEvent.UpdateVisibility(Visibility.Create(Visibility.VisibilityEnum.Private).payLoad);
            vEvent.UpdateDescription(Description.Create("").payLoad);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateLocationId(new LocationId(Guid.NewGuid()));
            Result<Status> resultStatus = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Ready).payLoad);

            Assert.Equal(0, resultStatus.resultCode);

            // Act S3
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);

            // Assert S3
            Assert.Equal("Nullam tempor lacus nisl, eget tempus", vEvent.Description.Value);
            Assert.Equal(Status.StatusEnum.Draft, vEvent.Status.Value);

            // Arrange F1
            // Act F1
            Result<Description> newDescriptionF1 = Description.Create("Nullam tempor lacus nisl, eget tempus \tquam maximus malesuada. Morbi faucibus \tsed neque vitae euismod. Vestibulum \tnon purus vel justo ornare vulputate. \tIn a interdum enim. Maecenas sed \tsodales elit, sit amet venenatis orci. \tSuspendisse potenti. Sed pulvinar \tturpis ut euismod varius. Nullam \tturpis tellus, tincidunt ut quam \tconvallis, auctor mollis nunc. Aliquam \terat volutpat.");
            // Assert F1
            Assert.Equal(20, newDescriptionF1.resultCode);

            // Arrange F2

            Assert.Equal(Status.StatusEnum.Draft, vEvent.Status.Value);
            Result<Status> resultStatusF2 = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Ready).payLoad);
            Assert.Equal(0, resultStatusF2.resultCode);
            Result<Status> resultStatusF2A = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Active).payLoad);
            Assert.Equal(0, resultStatusF2A.resultCode);

            // Act F2
            Result<Description> newDescriptionF2 = Description.Create("Nullam tempor lacus nisl, eget tempusi");

            // Assert F2
            Assert.Equal(21, vEvent.UpdateDescription(newDescriptionF2.payLoad).resultCode);
            Assert.Equal("Nullam tempor lacus nisl, eget tempus", vEvent.Description.Value);

            // Arrange F3
            Result<Status> resultStatusF3 = vEvent.UpdateStatus(Status.Create(Status.StatusEnum.Cancelled).payLoad);
            Assert.Equal(0, resultStatusF3.resultCode);

            // Act F3
            Result<Description> newDescriptionF3 = Description.Create("Nullam tempor");

            // Assert F3
            Assert.Equal(22, vEvent.UpdateDescription(newDescriptionF3.payLoad).resultCode);
            Assert.Equal("Nullam tempor lacus nisl, eget tempus", vEvent.Description.Value);
        }
    }
}
