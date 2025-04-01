using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event
{
    public class UpdateVisibilityTest
    {
        // ID 5 use case.
        [Fact]
        public void UpdateVisibility_Public()
        {
            // Arrange S1
            Title title = Title.Create("Event Title").payLoad;
            VEvent vEvent = VEvent.Create(Guid.NewGuid(), Status.Draft);
            vEvent.UpdateTitle(title);
            // Act S1
            Result<Visibility> resultVisibilityS1 = vEvent.UpdateVisibility(Visibility.Public);
            // Assert S1
            Assert.Equal(0, resultVisibilityS1.resultCode);
            Assert.Equal(Visibility.Public, vEvent.Visibility);

            // Arrange F1
            Result<Visibility> resultVisibilityF1A = vEvent.UpdateVisibility(Visibility.Private);
            Assert.Equal(0, resultVisibilityF1A.resultCode);
            Result<Status> resultStatusF1 = vEvent.UpdateStatus(Status.Cancelled);
            Assert.Equal(0, resultStatusF1.resultCode);
            // Act F1
            Result<Visibility> resultVisibilityF1B = vEvent.UpdateVisibility(Visibility.Public);
            // Assert F1
            Assert.Equal(41, resultVisibilityF1B.resultCode);
            Assert.Equal(Visibility.Private, vEvent.Visibility);
        }

        // ID 6 use case.
        [Fact]
        public void UpdateVisibility_Private()
        {
            // Arrange S1
            Title title = Title.Create("Event Title").payLoad;
            Status status = Status.Draft;
            VEvent vEvent = VEvent.Create(Guid.NewGuid(), status);
            vEvent.UpdateTitle(title);
            // Act S1
            Result<Visibility> resultVisibilityS1 = vEvent.UpdateVisibility(Visibility.Private);
            // Assert S1
            Assert.Equal(0, resultVisibilityS1.resultCode);
            Assert.Equal(Visibility.Private, vEvent.Visibility);
            // Arrange S2
            // Act S2
            Result<Visibility> resultVisibilityS2A = vEvent.UpdateVisibility(Visibility.Public);
            Assert.Equal(0, resultVisibilityS2A.resultCode);
            Result<Visibility> resultVisibilityS2B = vEvent.UpdateVisibility(Visibility.Private);
            // Assert S2
            Assert.Equal(Visibility.Private, vEvent.Visibility);
            Assert.Equal(Status.Draft, vEvent.Status);
            // Arrange F1
            Result<Visibility> resultVisibilityF1A = vEvent.UpdateVisibility(Visibility.Public);
            Assert.Equal(0, resultVisibilityF1A.resultCode);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(5).payLoad);
            vEvent.UpdateDescription(Description.Create("").payLoad);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            Result<Status> resultStatusF1A = vEvent.UpdateStatus(Status.Ready);
            Assert.Equal(0, resultStatusF1A.resultCode);
            Result<Status> resultStatusF1B = vEvent.UpdateStatus(Status.Active);
            Assert.Equal(0, resultStatusF1B.resultCode);
            // Act F1
            Result<Visibility> resultVisibilityF1B = vEvent.UpdateVisibility(Visibility.Private);
            // Assert F1
            Assert.Equal(42, resultVisibilityF1B.resultCode);
            Assert.Equal(Visibility.Public, vEvent.Visibility);
            // Arrange F2
            Result<Status> resultStatusF2 = vEvent.UpdateStatus(Status.Cancelled);
            Assert.Equal(0, resultStatusF2.resultCode);
            // Act F2
            Result<Visibility> resultVisibilityF2 = vEvent.UpdateVisibility(Visibility.Public);
            // Assert F2
            Assert.Equal(41, resultVisibilityF2.resultCode);
            Assert.Equal(Visibility.Public, vEvent.Visibility);
        }
    }
}
