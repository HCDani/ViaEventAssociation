using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Values;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts;
using System.Runtime;
using Moq;
using UnitTests.Features.Tools.Fakes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ViaEventAssociation.Core.Domain.Services;

namespace UnitTests.Features.EventParticipationTest {
    public class EventParticipationTest {
        // ID 11 use case.
        [Fact]
        public async Task GuestParticipatesEvent() {
            // Arrange S1
            Guid id = Guid.NewGuid();
            GuestName guestName = GuestName.Create("John", "Doe").payLoad;
            Email email = Email.Create("john@via.dk").payLoad;
            ProfilePictureUrl profilePictureUrl = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;
            Result<Guest> guestResult = Guest.RegisterGuest(id, guestName, email, profilePictureUrl);
            Guid id2 = Guid.NewGuid();
            GuestName guestName2 = GuestName.Create("John", "Doe").payLoad;
            Email email2 = Email.Create("john@via.dk").payLoad;
            ProfilePictureUrl profilePictureUrl2 = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;
            Result<Guest> guestResult2 = Guest.RegisterGuest(id2, guestName2, email2, profilePictureUrl2);
            Guid id3 = Guid.NewGuid();
            GuestName guestName3 = GuestName.Create("John", "Doe").payLoad;
            Email email3 = Email.Create("john@via.dk").payLoad;
            ProfilePictureUrl profilePictureUrl3 = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;
            Result<Guest> guestResult3 = Guest.RegisterGuest(id3, guestName3, email3, profilePictureUrl3);
            Guid id4 = Guid.NewGuid();
            GuestName guestName4 = GuestName.Create("John", "Doe").payLoad;
            Email email4 = Email.Create("john@via.dk").payLoad;
            ProfilePictureUrl profilePictureUrl4 = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;
            Result<Guest> guestResult4 = Guest.RegisterGuest(id4, guestName4, email4, profilePictureUrl4);
            Guid id5 = Guid.NewGuid();
            GuestName guestName5 = GuestName.Create("John", "Doe").payLoad;
            Email email5 = Email.Create("john@via.dk").payLoad;
            ProfilePictureUrl profilePictureUrl5 = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;
            Result<Guest> guestResult5 = Guest.RegisterGuest(id5, guestName5, email5, profilePictureUrl5);

            VEvent vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(Title.Create("Event Title").payLoad);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Public);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            Result<EventStatus> eventStatusResultS1 = vEvent.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultS1.resultCode);
            Result<EventStatus> eventStatusResultS2 = vEvent.UpdateStatus(EventStatus.Active);
            Assert.Equal(0, eventStatusResultS2.resultCode);
            InMemEventParticipationStub mockEventParticipants = new InMemEventParticipationStub();

            // Act S1
            Result<EventParticipation> eventParticipationResult = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);

            // Assert S1
            Assert.Equal(0, eventParticipationResult.resultCode);

            // Arrange F1
            vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateVisibility(Visibility.Public);

            // Act F1
            Result<EventParticipation> eventParticipationResultF1 = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);

            // Assert F1
            Assert.Equal(154, eventParticipationResultF1.resultCode);

            // Arrange F5
            Title titleF2 = Title.Create("Event Title").payLoad;
            vEvent.UpdateTitle(titleF2);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            Result<EventStatus> eventStatusResultF2A = vEvent.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultF2A.resultCode);
            Result<EventStatus> eventStatusResultF2B = vEvent.UpdateStatus(EventStatus.Active);
            Assert.Equal(0, eventStatusResultF2B.resultCode);

            Result<EventParticipation> eventParticipationResultF5A = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);
            await mockEventParticipants.CreateAsync(eventParticipationResultF5A.payLoad);

            // Act F5
            Result<EventParticipation> eventParticipationResultF5 = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);
            // Assert F5
            Assert.Equal(153, eventParticipationResultF5.resultCode);
            // Arrange F2
            Result<EventParticipation> eventParticipationResultF2B = await EventParticipation.Create(Guid.NewGuid(), guestResult2.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2B.payLoad);
            Result<EventParticipation> eventParticipationResultF2C = await EventParticipation.Create(Guid.NewGuid(), guestResult3.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2C.payLoad);
            Result<EventParticipation> eventParticipationResultF2D = await EventParticipation.Create(Guid.NewGuid(), guestResult4.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2D.payLoad);
            Result<EventParticipation> eventParticipationResultF2E = await EventParticipation.Create(Guid.NewGuid(), guestResult5.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2E.payLoad);

            Guid id6 = Guid.NewGuid();
            GuestName guestName6 = GuestName.Create("John", "Doe").payLoad;
            Email email6 = Email.Create("john@via.dk").payLoad;
            ProfilePictureUrl profilePictureUrl6 = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;
            Result<Guest> guestResult6 = Guest.RegisterGuest(id6, guestName6, email6, profilePictureUrl6);
            // Act F2
            Result<EventParticipation> eventParticipationResultF2F = await EventParticipation.Create(Guid.NewGuid(), guestResult6.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);

            // Assert F2
            Assert.Equal(155, eventParticipationResultF2F.resultCode);
            // Arrange F3 This only works if the current time is after 8 am and before midnight by 2 seconds.
                FakeSystemTime fakeSystemTime = new FakeSystemTime();
                fakeSystemTime.DateTime = new DateTime(2026, 05, 27, 9, 0, 0);
                SystemTimeHolder.SetSystemTime(fakeSystemTime);
                vEvent = VEvent.Create(Guid.NewGuid());
                vEvent.UpdateTitle(Title.Create("Event Title").payLoad);
                vEvent.UpdateVisibility(Visibility.Public);
                vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
                vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
                vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
                Result<EventDuration> newDurationF3 = EventDuration.Create(new DateTime(2026, 05, 27, 10, 0, 0), new DateTime(2026, 05, 27, 11, 0, 0));
                vEvent.UpdateDuration(newDurationF3.payLoad);
                Assert.Equal(0, vEvent.UpdateDuration(newDurationF3.payLoad).resultCode);
                Result<EventStatus> eventStatusResultF3A = vEvent.UpdateStatus(EventStatus.Ready);
                Assert.Equal(0, eventStatusResultF3A.resultCode);
                Result<EventStatus> eventStatusResultF3B = vEvent.UpdateStatus(EventStatus.Active);
                Assert.Equal(0, eventStatusResultF3B.resultCode);
                fakeSystemTime.DateTime = new DateTime(2026, 05, 27, 10, 01, 0);

            // Act F3
            Result<EventParticipation> eventParticipationResultF3 = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);

                // Assert F3
                Assert.Equal(156, eventParticipationResultF3.resultCode);

            // Arrange F4
            vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(Title.Create("Event Title").payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            Result<EventDuration> newDurationF4 = EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11));
            vEvent.UpdateDuration(newDurationF4.payLoad);
            Assert.Equal(0, vEvent.UpdateDuration(newDurationF4.payLoad).resultCode);
            Result<EventStatus> eventStatusResultF4A = vEvent.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultF4A.resultCode);
            Result<EventStatus> eventStatusResultF4B = vEvent.UpdateStatus(EventStatus.Active);
            Assert.Equal(0, eventStatusResultF4B.resultCode);

            // Act F4
            Result<EventParticipation> eventParticipationResultF4 = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Participating, mockEventParticipants);

            // Assert F4
            Assert.Equal(158, eventParticipationResultF4.resultCode);
        }

        // TODO: ID 12 use case.
        [Fact]
        public async Task GuestCancelsParticipation() {
            // Arrange S1
            FakeSystemTime fakeSystemTime = new FakeSystemTime();
            fakeSystemTime.DateTime = new DateTime(2026, 05, 27, 9, 0, 0);
            SystemTimeHolder.SetSystemTime(fakeSystemTime);
            VEvent vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(Title.Create("Event Title").payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            Result<EventDuration> newDurationS1 = EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11));
            vEvent.UpdateDuration(newDurationS1.payLoad);
            Assert.Equal(0, vEvent.UpdateDuration(newDurationS1.payLoad).resultCode);
            Result<EventStatus> eventStatusResultS1A = vEvent.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultS1A.resultCode);
            Result<EventStatus> eventStatusResultS1B = vEvent.UpdateStatus(EventStatus.Active);
            Assert.Equal(0, eventStatusResultS1B.resultCode);

            Guid id = Guid.NewGuid();
            GuestName guestName = GuestName.Create("John", "Doe").payLoad;
            Email email = Email.Create("john@via.dk").payLoad;
            ProfilePictureUrl profilePictureUrl = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;
            Result<Guest> guestResult = Guest.RegisterGuest(id, guestName, email, profilePictureUrl);
            InMemEventParticipationStub mockEventParticipants = new InMemEventParticipationStub();
            Result<EventParticipation> eventParticipationResultS1A = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.True(eventParticipationResultS1A.IsSuccess());
            await mockEventParticipants.CreateAsync(eventParticipationResultS1A.payLoad);

            // Act S1
            Result<ParticipationStatus> eventParticipationStatusResultS1A = await eventParticipationResultS1A.payLoad.UpdateStatus(ParticipationStatus.Cancelled, mockEventParticipants);

            // Assert S1
            Assert.Equal(0, eventParticipationStatusResultS1A.resultCode);

            // Arrange S2 There is no such cases where the system would care about a guest that is not part of the system at all.

            // Arrange F1
            mockEventParticipants.Clear();
            Result<EventParticipation> eventParticipationResultF1A = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.True(eventParticipationResultF1A.IsSuccess());
            await mockEventParticipants.CreateAsync(eventParticipationResultF1A.payLoad);
            Result<ParticipationStatus> eventParticipationStatusResultF1A = await eventParticipationResultF1A.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF1A.resultCode);

            // Act F1
            fakeSystemTime.DateTime = new DateTime(2026, 10, 31, 10, 0, 0);
            Result<ParticipationStatus> eventParticipationResultF1B = await eventParticipationResultF1A.payLoad.UpdateStatus(ParticipationStatus.Cancelled, mockEventParticipants);

            // Assert F1
            Assert.Equal(156, eventParticipationResultF1B.resultCode);

        }

        // ID 13 use case.
        [Fact]
        public async Task GuestIsInvitedToEvent() {
            // Arrange S1
            VEvent vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(Title.Create("Event Title").payLoad);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Public);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            Result<EventStatus> eventStatusResultS1 = vEvent.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultS1.resultCode);
            InMemEventParticipationStub mockEventParticipants = new InMemEventParticipationStub();

            Guest guest = Guest.RegisterGuest(Guid.NewGuid(),GuestName.Create("Alma","Beta").payLoad,Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;

            // Act S1
            Result<EventParticipation> eventParticipationResult = await EventParticipation.Create(Guid.NewGuid(), guest, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            // Assert S1
            Assert.Equal(0, eventParticipationResult.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResult.payLoad);

            // Arrange F3
            // Act F3
            Result<EventParticipation> eventParticipationResultF3 = await EventParticipation.Create(Guid.NewGuid(), guest, vEvent, ParticipationStatus.Invited, mockEventParticipants);

            // Assert F3
            Assert.Equal(153, eventParticipationResultF3.resultCode);

            // Arrange F4
            Result<EventStatus> eventStatusResultF1 = vEvent.UpdateStatus(EventStatus.Active);
            Assert.Equal(0, eventStatusResultF1.resultCode);
            Result<ParticipationStatus> eventParticipationStatusResult = await eventParticipationResult.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResult.resultCode);

            // Act F4
            eventParticipationResult = await EventParticipation.Create(Guid.NewGuid(), guest, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            // Assert F4
            Assert.Equal(153, eventParticipationResult.resultCode);

            // Arrange F2
            Guest guest2 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Guest guest3 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Guest guest4 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Guest guest5 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Guest guest6 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
 

            // Act F2
            Result<EventParticipation> eventParticipationResultF2A = await EventParticipation.Create(Guid.NewGuid(), guest2, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF2A.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2A.payLoad);
            Result<ParticipationStatus> eventParticipationStatusResultF2A = await eventParticipationResultF2A.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF2A.resultCode);

            Result<EventParticipation> eventParticipationResultF2B = await EventParticipation.Create(Guid.NewGuid(), guest3, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF2B.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2B.payLoad);
            Result<ParticipationStatus> eventParticipationStatusResultF2B = await eventParticipationResultF2B.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF2B.resultCode);

            Result<EventParticipation> eventParticipationResultF2C = await EventParticipation.Create(Guid.NewGuid(), guest4, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF2C.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2C.payLoad);
            Result<ParticipationStatus> eventParticipationStatusResultF2C = await eventParticipationResultF2C.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF2C.resultCode);

            Result<EventParticipation> eventParticipationResultF2D = await EventParticipation.Create(Guid.NewGuid(), guest5, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF2D.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2D.payLoad);
            Result<ParticipationStatus> eventParticipationStatusResultF2D = await eventParticipationResultF2D.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF2D.resultCode);

            Result<EventParticipation> eventParticipationResultF2E = await EventParticipation.Create(Guid.NewGuid(), guest6, vEvent, ParticipationStatus.Invited, mockEventParticipants);

            // Assert F2
            Assert.Equal(155, eventParticipationResultF2E.resultCode);

            // Arrange F1
            eventStatusResultS1 = vEvent.UpdateStatus(EventStatus.Cancelled);

            // Act F1
            Result<EventParticipation> eventParticipationResultF1 = await EventParticipation.Create(Guid.NewGuid(), guest, vEvent, ParticipationStatus.Invited, mockEventParticipants);

            // Assert F1
            Assert.Equal(157, eventParticipationResultF1.resultCode);
        }

        // ID 14 use case.
        [Fact]
        public async Task GuestAcceptsInvitation() {
            // Arrange S1
            FakeSystemTime fakeSystemTime = new FakeSystemTime();
            fakeSystemTime.DateTime = new DateTime(2026, 05, 27, 9, 0, 0);
            SystemTimeHolder.SetSystemTime(fakeSystemTime);
            VEvent vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(Title.Create("Event Title").payLoad);
            vEvent.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEvent.UpdateVisibility(Visibility.Public);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            Result<EventStatus> eventStatusResultS1 = vEvent.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultS1.resultCode);
            Result<EventStatus> eventStatusResultS1B = vEvent.UpdateStatus(EventStatus.Active);
            Assert.Equal(0, eventStatusResultS1B.resultCode);
            InMemEventParticipationStub mockEventParticipants = new InMemEventParticipationStub();

            Guest guest = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Result<EventParticipation> eventParticipationResult = await EventParticipation.Create(Guid.NewGuid(), guest, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResult.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResult.payLoad);

            // Act S1
            Result<ParticipationStatus> eventParticipationStatusResultS1 = await eventParticipationResult.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);

            // Assert S1
            Assert.Equal(0, eventParticipationStatusResultS1.resultCode);

            // Arrange F1, Act F1, Assert F1 Invitation acceptance is called on the participation object itself, therefore it's not possible to get a "not found" participation.

            // Arrange F2
            Guest guest2 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Guest guest3 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Guest guest4 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Guest guest5 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;
            Guest guest6 = Guest.RegisterGuest(Guid.NewGuid(), GuestName.Create("Alma", "Beta").payLoad, Email.Create("alma@via.dk").payLoad, ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad).payLoad;

            Result<EventParticipation> eventParticipationResultF2A = await EventParticipation.Create(Guid.NewGuid(), guest2, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF2A.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2A.payLoad);
            Result<ParticipationStatus> eventParticipationStatusResultF2A = await eventParticipationResultF2A.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF2A.resultCode);

            Result<EventParticipation> eventParticipationResultF2B = await EventParticipation.Create(Guid.NewGuid(), guest3, vEvent, ParticipationStatus.Invited, mockEventParticipants);   
            Assert.Equal(0, eventParticipationResultF2B.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2B.payLoad);
            Result<ParticipationStatus> eventParticipationStatusResultF2B = await eventParticipationResultF2B.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF2B.resultCode);

            Result<EventParticipation> eventParticipationResultF2C = await EventParticipation.Create(Guid.NewGuid(), guest4, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF2C.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2C.payLoad);
            Result<ParticipationStatus> eventParticipationStatusResultF2C = await eventParticipationResultF2C.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF2C.resultCode);

            Result<EventParticipation> eventParticipationResultF2D = await EventParticipation.Create(Guid.NewGuid(), guest5, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF2D.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2D.payLoad);
            Result<EventParticipation> eventParticipationResultF2E = await EventParticipation.Create(Guid.NewGuid(), guest6, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF2E.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF2E.payLoad);

            Result<ParticipationStatus> eventParticipationStatusResultF2D = await eventParticipationResultF2D.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);
            Assert.Equal(0, eventParticipationStatusResultF2D.resultCode);

            // Act F2

            Result<ParticipationStatus> eventParticipationStatusResultF2F = await eventParticipationResultF2E.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);

            // Assert F2
            Assert.Equal(155, eventParticipationStatusResultF2F.resultCode);

            // Arrange F4
            VEvent vEventF4 = VEvent.Create(Guid.NewGuid());
            vEventF4.UpdateTitle(Title.Create("Event Title").payLoad);
            vEventF4.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEventF4.UpdateVisibility(Visibility.Public);
            vEventF4.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEventF4.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            Result<EventStatus> eventStatusResultF4 = vEventF4.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultF4.resultCode);
            mockEventParticipants.Clear();
            Result<EventParticipation> eventParticipationResultF4 = await EventParticipation.Create(Guid.NewGuid(), guest2, vEventF4, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF4.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF4.payLoad);

            // Act F4
            Result<ParticipationStatus> eventParticipationResultF4F = await eventParticipationResultF4.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);

            // Assert F4
            Assert.Equal(161, eventParticipationResultF4F.resultCode);

            // Arrange F3
            Result<EventParticipation> eventParticipationResultF3 = await EventParticipation.Create(Guid.NewGuid(), guest6, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF3.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF3.payLoad);

            // Act F3
            Result<EventStatus> eventStatusResultF3 = vEvent.UpdateStatus(EventStatus.Cancelled);
            Assert.Equal(0, eventStatusResultF3.resultCode);
            Result<ParticipationStatus> eventParticipationResultF3F = await eventParticipationResultF3.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);

            // Assert F3
            Assert.Equal(157, eventParticipationResultF3F.resultCode);

            // Arrange F5

            VEvent vEventF5 = VEvent.Create(Guid.NewGuid());
            vEventF5.UpdateTitle(Title.Create("Event Title").payLoad);
            vEventF5.UpdateDuration(EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11)).payLoad);
            vEventF5.UpdateVisibility(Visibility.Public);
            vEventF5.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEventF5.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            Result<EventStatus> eventStatusResultF5 = vEventF5.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultF5.resultCode);
            Result<EventStatus> eventStatusResultF5A = vEventF5.UpdateStatus(EventStatus.Active);
            Assert.Equal(0, eventStatusResultF5A.resultCode);
            mockEventParticipants.Clear();
            Result<EventParticipation> eventParticipationResultF5 = await EventParticipation.Create(Guid.NewGuid(), guest2, vEventF5, ParticipationStatus.Invited, mockEventParticipants);
            Assert.Equal(0, eventParticipationResultF5.resultCode);
            await mockEventParticipants.CreateAsync(eventParticipationResultF5.payLoad);

            fakeSystemTime.DateTime = new DateTime(2026, 10, 31, 10, 0, 0);

            // Act F5
            Result<ParticipationStatus> eventParticipationResultF5F = await eventParticipationResultF5.payLoad.UpdateStatus(ParticipationStatus.Participating, mockEventParticipants);

            // Assert F5
            Assert.Equal(156, eventParticipationResultF5F.resultCode);
        }
        // ID 15 use case
        [Fact]
        public async Task GuestDeclinesInvitation() {
            // Arrange S1
            FakeSystemTime fakeSystemTime = new FakeSystemTime();
            fakeSystemTime.DateTime = new DateTime(2026, 05, 27, 9, 0, 0);
            SystemTimeHolder.SetSystemTime(fakeSystemTime);
            VEvent vEvent = VEvent.Create(Guid.NewGuid());
            vEvent.UpdateTitle(Title.Create("Event Title").payLoad);
            vEvent.UpdateVisibility(Visibility.Private);
            vEvent.UpdateDescription(Description.Create("Nullam tempor lacus nisl, eget tempus").payLoad);
            vEvent.UpdateLocation(Location.Create(Guid.NewGuid()).payLoad);
            vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(10).payLoad);
            Result<EventDuration> newDurationS1 = EventDuration.Create(new DateTime(2026, 10, 31, 9, 0, 0), new DateTime(2026, 10, 31, 11, 11, 11));
            vEvent.UpdateDuration(newDurationS1.payLoad);
            Assert.Equal(0, vEvent.UpdateDuration(newDurationS1.payLoad).resultCode);
            Result<EventStatus> eventStatusResultS1A = vEvent.UpdateStatus(EventStatus.Ready);
            Assert.Equal(0, eventStatusResultS1A.resultCode);
            Result<EventStatus> eventStatusResultS1B = vEvent.UpdateStatus(EventStatus.Active);
            Assert.Equal(0, eventStatusResultS1B.resultCode);

            Guid id = Guid.NewGuid();
            GuestName guestName = GuestName.Create("John", "Doe").payLoad;
            Email email = Email.Create("john@via.dk").payLoad;
            ProfilePictureUrl profilePictureUrl = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;
            Result<Guest> guestResult = Guest.RegisterGuest(id, guestName, email, profilePictureUrl);
            InMemEventParticipationStub mockEventParticipants = new InMemEventParticipationStub();
            Result<EventParticipation> eventParticipationResultS1A = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.True(eventParticipationResultS1A.IsSuccess());
            await mockEventParticipants.CreateAsync(eventParticipationResultS1A.payLoad);

            // Act S1
            Result<ParticipationStatus> eventParticipationStatusResultS1A = await eventParticipationResultS1A.payLoad.UpdateStatus(ParticipationStatus.Declined, mockEventParticipants);

            // Assert S1
            Assert.Equal(0, eventParticipationStatusResultS1A.resultCode);

            // Arrange S2 If the guest has accepted the invitation, they are part of the event, if they wish to leave it, they have to cancel. There is no such thing as declining an already accepted invitation.

            // Arrange F1 There is no such case where the guest could accept a non-existent invitation.

            // Arrange F2
            mockEventParticipants.Clear();
            Result<EventParticipation> eventParticipationResultF2A = await EventParticipation.Create(Guid.NewGuid(), guestResult.payLoad, vEvent, ParticipationStatus.Invited, mockEventParticipants);
            Assert.True(eventParticipationResultF2A.IsSuccess());
            await mockEventParticipants.CreateAsync(eventParticipationResultF2A.payLoad);

            // Act F2
            Result<EventStatus> eventStatusResultF2 = vEvent.UpdateStatus(EventStatus.Cancelled);
            Assert.Equal(0, eventStatusResultF2.resultCode);
            Result<ParticipationStatus> eventParticipationResultF2B = await eventParticipationResultF2A.payLoad.UpdateStatus(ParticipationStatus.Declined, mockEventParticipants);

            // Assert F2
            Assert.Equal(157, eventParticipationResultF2B.resultCode);

        }
    }
}
