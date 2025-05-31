using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Infrastructure.Persistence.Contracts;
using ViaEventAssociation.Infrastructure.Persistence.Repositories;

namespace IntegrationTests.RepositoryTests {
    [TestClass]
    public class JSONLoader {
        private const string Dbname = "../../../JSONLoaderTest.db";
        [TestMethod]
        public async Task TestJSONLoader() {
            // Arrange
            using (var ctx = GlobalUsings.CreateDbContext(Dbname)) {
                GlobalUsings.InitializeDatabase(ctx);
                LocationRepository locationRepository = new(ctx);
                string locationFileName = "../../../testdata/Locations.json";
                string locationJsonString = File.ReadAllText(locationFileName);
                JsonArray locationsNode = JsonNode.Parse(locationJsonString)!.AsArray();
                //Id, Name, MaxCapacity, AvailabilityStart(day(2 digits)-month-year), AvailabilityEnd
                for(int i = 0; i < locationsNode.Count; i++) {
                    JsonObject locationNode = locationsNode[i]!.AsObject();
                    string startStr = locationNode["AvailabilityStart"]!.ToString()!;
                    string endStr = locationNode["AvailabilityEnd"]!.ToString()!;
                    DateTime start, end;

                    bool startOk = DateTime.TryParseExact(startStr, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start);
                    bool endOk = DateTime.TryParseExact(endStr, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end);

                    if (!startOk || !endOk) {
                        // Handle the invalid date case: skip, log, or use a fallback value
                        Console.WriteLine($"Invalid date(s): {startStr}, {endStr} -- skipping entry.");
                        continue; // Skip this entry
                    }

                    // Create Location object from JSON
                    // Guid id, LocationName newName, MaxCapacity newCapacity, Availability newAvailability, Address newAddress
                    Result<Location> location = Location.Create(Guid.Parse(locationNode["Id"]!.ToString()!),
                        LocationName.Create(locationNode["Name"]!.ToString()!).payLoad,
                        MaxCapacity.Create(int.Parse(locationNode["MaxCapacity"]!.ToString()!)).payLoad,
                        Availability.Create(start, end).payLoad,
                        Address.Create(1000, "City", "street", 69).payLoad);
                    await locationRepository.CreateAsync(location.payLoad);
                }
                
                string eventFileName = "../../../testdata/Events.json";
                string eventJsonString = File.ReadAllText(eventFileName);
                JsonArray eventsNode = JsonNode.Parse(eventJsonString)!.AsArray();
                for(int i = 0; i < eventsNode.Count; i++) {
                    JsonObject eventNode = eventsNode[i]!.AsObject();
                    string startStr = eventNode["Start"]!.ToString()!;
                    string endStr = eventNode["End"]!.ToString()!;
                    DateTime start, end;

                    bool startOk = DateTime.TryParseExact(startStr, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start);
                    bool endOk = DateTime.TryParseExact(endStr, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end);

                    if (!startOk || !endOk) {
                        // Handle the invalid date case: skip, log, or use a fallback value
                        Console.WriteLine($"Invalid date(s): {startStr}, {endStr} -- skipping entry.");
                        continue; // Skip this entry
                    }
                    // Id, Title,Description, Status, Visibility, Start(year-month-day(2 digits), hour:minutes(2 digits)), End, MaxGuests, LocationId
                    VEvent vEvent = VEvent.Create(Guid.Parse(eventNode["Id"]!.ToString()!));
                    vEvent.UpdateTitle(Title.Create(eventNode["Title"]!.ToString()!).payLoad);
                    vEvent.UpdateDescription(Description.Create(eventNode["Description"]!.ToString()!).payLoad);
                    string statusString = eventNode["Status"]!.ToString()!;
                    if (Enum.TryParse<EventStatus>(statusString, out EventStatus eventStatus)) {
                        vEvent.UpdateStatus(eventStatus);
                    }
                    string visibilityString = eventNode["Status"]!.ToString()!;
                    if (Enum.TryParse<Visibility>(visibilityString, out Visibility eventVisibility)) {
                        vEvent.UpdateVisibility(eventVisibility);
                    }
                    vEvent.UpdateDuration(EventDuration.Create(start, end).payLoad);
                    vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(int.Parse(eventNode["MaxNumberOfGuests"]!.ToString()!)).payLoad);
                    vEvent.UpdateLocation(eventNode["LocationId"] != null ? locationRepository.GetAsync(Guid.Parse(eventNode["LocationId"]!.ToString()!)).Result : null);

                    GuestRepository guestRepository = new(ctx);
                    string guestFileName = "../../../testdata/Guests.json";
                    string guestJsonString = File.ReadAllText(guestFileName);
                    JsonArray guestsNode = JsonNode.Parse(guestJsonString)!.AsArray();
                    for (int j = 0; j < guestsNode.Count; j++) {
                        JsonObject guestNode = guestsNode[j]!.AsObject();
                        // Id, FirstName, LastName, Email, ProfilePictureUrl
                        Result<Guest> guest = Guest.RegisterGuest(Guid.Parse(guestNode["Id"]!.ToString()!),
                            GuestName.Create(guestNode["FirstName"]!.ToString()!, guestNode["LastName"]!.ToString()!).payLoad,
                            Email.Create(guestNode["Email"]!.ToString()!).payLoad,
                            ProfilePictureUrl.Create(guestNode["ProfilePictureUrl"]!.ToString()!).payLoad);
                        await guestRepository.CreateAsync(guest.payLoad);
                    }
                }
                string invitationFileName = "../../../testdata/Invitations.json";
                string invitationJsonString = File.ReadAllText(invitationFileName);
                JsonArray invitationsNode = JsonNode.Parse(invitationJsonString)!.AsArray();
                EventParticipantsContract eventParticipants = new EventParticipantsContract(ctx);
                for (int i = 0; i < invitationsNode.Count; i++) {
                    JsonObject invitationNode = invitationsNode[i]!.AsObject();
                    // Id, EventId, GuestId, ParticipationStatus
                    string participationStatusString = invitationNode["Status"]!.ToString()!;
                    if (Enum.TryParse<ParticipationStatus>(participationStatusString, out ParticipationStatus participationStatus)) {
                        EventParticipation eventParticipation1 = EventParticipation.Create(Guid.NewGuid(),
                            Guid.Parse(invitationNode["EventId"]!.ToString()!),
                            Guid.Parse(invitationNode["GuestId"]!.ToString()!),
                            participationStatus,eventParticipants.GetParticipants()).payLoad;
                    }


                }
            }
        }
    }
}
