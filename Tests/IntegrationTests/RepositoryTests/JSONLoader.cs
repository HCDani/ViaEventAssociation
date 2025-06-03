using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ViaEventAssociation.Infrastructure.Queries.Models;
using ViaEventAssociation.Core.Tools.SystemTime;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Infrastructure.Persistence.Contracts;
using ViaEventAssociation.Infrastructure.Persistence.Repositories;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Infrastructure.Queries.Persistence;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Values;

namespace IntegrationTests.RepositoryTests {
    [TestClass]
    public class JSONLoader {
        private const string Dbname = "../../../JSONLoaderTest.db";
        [TestMethod]
        public async Task TestJSONLoader() {
            // Arrange
            using (var ctx = ScaffoldingDbContextFactory.CreateDbContext(Dbname)) {
                await SeedDatabaseFromJson(ctx);
            }
        }

        public static async Task SeedDatabaseFromJson(ScaffoldingDbinitContext ctx) {
            ScaffoldingDbContextFactory.InitializeDatabase(ctx);
            string locationFileName = "../../../testdata/Locations.json";
            string locationJsonString = File.ReadAllText(locationFileName);
            JsonArray locationsNode = JsonNode.Parse(locationJsonString)!.AsArray();
            //Id, Name, MaxCapacity, AvailabilityStart(day(2 digits)-month-year), AvailabilityEnd
            for (int i = 0; i < locationsNode.Count; i++) {
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
                Location location = new Location() {
                    Id = Guid.Parse(locationNode["Id"]!.ToString()!),
                    LocationName = locationNode["Name"]!.ToString()!,
                    MaxCapacity = int.Parse(locationNode["MaxCapacity"]!.ToString()!),
                    AvailabilityFrom = start,
                    AvailabilityTo = end,
                    PostalCode = 1000,
                    City = "City",
                    Street = "street",
                    HouseNumber = 69
                };
                await ctx.Set<Location>().AddAsync(location);
                await ctx.SaveChangesAsync();
            }

            string eventFileName = "../../../testdata/Events.json";
            string eventJsonString = File.ReadAllText(eventFileName);
            JsonArray eventsNode = JsonNode.Parse(eventJsonString)!.AsArray();
            for (int i = 0; i < eventsNode.Count; i++) {
                JsonObject eventNode = eventsNode[i]!.AsObject();
                string startStr = eventNode["Start"]!.ToString()!;
                string endStr = eventNode["End"]!.ToString()!;
                DateTime start, end;

                bool startOk = DateTime.TryParseExact(startStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out start);
                bool endOk = DateTime.TryParseExact(endStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out end);

                if (!startOk || !endOk) {
                    // Handle the invalid date case: skip, log, or use a fallback value
                    Console.WriteLine($"Invalid date(s): {startStr}, {endStr} -- skipping entry.");
                    continue; // Skip this entry
                }
                // Id, Title,Description, Status, Visibility, Start(year-month-day(2 digits), hour:minutes(2 digits)), End, MaxGuests, LocationId
                string statusString = eventNode["Status"]!.ToString()!;
                if (!Enum.TryParse<EventStatus>(statusString, out EventStatus eventStatus)) {
                    eventStatus = EventStatus.Draft; // Default to Draft if parsing fails
                }
                string visibilityString = eventNode["Visibility"]!.ToString()!;
                if (!Enum.TryParse<Visibility>(visibilityString, out Visibility eventVisibility)) {
                    eventVisibility = Visibility.Private; // Default to Private if parsing fails
                }
                Event vEvent = new Event() {
                    Id = Guid.Parse(eventNode["Id"]!.ToString()!),
                    Title = eventNode["Title"]!.ToString()!,
                    Description = eventNode["Description"]!.ToString()!,
                    Status = (int)eventStatus,
                    Visibility = (int)eventVisibility,
                    DurationFrom = start,
                    DurationTo = end,
                    MaxNumberOfGuests = int.Parse(eventNode["MaxGuests"]!.ToString()!),
                    LocationId = eventNode["LocationId"] != null ? Guid.Parse(eventNode["LocationId"]!.ToString()!) : (Guid?)null
                };
                await ctx.Set<Event>().AddAsync(vEvent);
                await ctx.SaveChangesAsync();
            }
            string guestFileName = "../../../testdata/Guests.json";
            string guestJsonString = File.ReadAllText(guestFileName);
            JsonArray guestsNode = JsonNode.Parse(guestJsonString)!.AsArray();
            for (int j = 0; j < guestsNode.Count; j++) {
                JsonObject guestNode = guestsNode[j]!.AsObject();
                // Id, FirstName, LastName, Email, ProfilePictureUrl
                Guest guest = new Guest() {
                    Id = Guid.Parse(guestNode["Id"]!.ToString()!),
                    FirstName = guestNode["FirstName"]!.ToString()!,
                    LastName = guestNode["LastName"]!.ToString()!,
                    Email = guestNode["Email"]!.ToString()!,
                    Ppurl = guestNode["Url"]!.ToString()!
                };
                await ctx.Set<Guest>().AddAsync(guest);
                await ctx.SaveChangesAsync();
            }

            string invitationFileName = "../../../testdata/Invitations.json";
            string invitationJsonString = File.ReadAllText(invitationFileName);
            JsonArray invitationsNode = JsonNode.Parse(invitationJsonString)!.AsArray();
            for (int i = 0; i < invitationsNode.Count; i++) {
                JsonObject invitationNode = invitationsNode[i]!.AsObject();
                // EventId, GuestId, Status
                string participationStatusString = invitationNode["Status"]!.ToString()!;
                if (!Enum.TryParse<ParticipationStatus>(participationStatusString, out ParticipationStatus participationStatus)) {
                    participationStatus = ParticipationStatus.Invited; // Default to Invited if parsing fails
                }
                EventParticipation eventParticipation = new EventParticipation() {
                    Id = Guid.NewGuid(),
                    GuestId = Guid.Parse(invitationNode["GuestId"]!.ToString()!),
                    EventId = Guid.Parse(invitationNode["EventId"]!.ToString()!),
                    ParticipationStatus = (int)participationStatus
                };
                await ctx.Set<EventParticipation>().AddAsync(eventParticipation);
                await ctx.SaveChangesAsync();

            }
            string participationFileName = "../../../testdata/Participations.json";
            string participationJsonString = File.ReadAllText(participationFileName);
            JsonArray participationsNode = JsonNode.Parse(participationJsonString)!.AsArray();
            for (int i = 0; i < participationsNode.Count; i++) {
                JsonObject participationNode = participationsNode[i]!.AsObject();
                // EventId, GuestId,
                EventParticipation eventParticipation = new EventParticipation() {
                    Id = Guid.NewGuid(),
                    GuestId = Guid.Parse(participationNode["GuestId"]!.ToString()!),
                    EventId = Guid.Parse(participationNode["EventId"]!.ToString()!),
                    ParticipationStatus = 0 // 0 is the default value for ParticipationStatus.Participating
                };
                await ctx.Set<EventParticipation>().AddAsync(eventParticipation);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
