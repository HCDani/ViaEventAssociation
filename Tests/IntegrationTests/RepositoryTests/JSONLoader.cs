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
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
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
                    // Id, Title, Status, Visibility, Start(year-month-day(2 digits), hour:minutes(2 digits)), End, MaxGuests, LocationId
                    VEvent vEvent = VEvent.Create(Guid.Parse(eventNode["Id"]!.ToString()!));
                    vEvent.UpdateTitle(Title.Create(eventNode["Title"]!.ToString()!).payLoad);
                    vEvent.UpdateDescription(Description.Create(eventNode["Description"]!.ToString()!).payLoad);
                    vEvent.UpdateDuration(EventDuration.Create(start, end).payLoad);
                    vEvent.UpdateMaxNumberOfGuests(MaxNumberOfGuests.Create(int.Parse(eventNode["MaxNumberOfGuests"]!.ToString()!)).payLoad);
                    vEvent.UpdateStatus(EventStatus.Create(int.Parse(eventNode["Status"]!.ToString()!)).payLoad);


                    eventNode["Title"]!.ToString()!,
                        eventNode["Description"]!.ToString()!,
                        start,
                        end,
                        int.Parse(eventNode["Status"]!.ToString()!),
                        int.Parse(eventNode["Visibility"]!.ToString()!),
                        int.Parse(eventNode["MaxNumberOfGuests"]!.ToString()!),
                        null // Assuming Location is not provided in the JSON
                    /*VEvent vEvent = new VEvent(
                        Guid.Parse(eventNode["Id"]!.ToString()!),
                        eventNode["Title"]!.ToString()!,
                        eventNode["Description"]!.ToString()!,
                        DateTime.Parse(eventNode["DurationFrom"]!.ToString()!),
                        DateTime.Parse(eventNode["DurationTo"]!.ToString()!),
                        int.Parse(eventNode["Status"]!.ToString()!),
                        int.Parse(eventNode["Visibility"]!.ToString()!),
                        int.Parse(eventNode["MaxNumberOfGuests"]!.ToString()!),
                        null // Assuming Location is not provided in the JSON
                    );
                    ctx.Add(vEvent);*/
                }

            }
        }
    }
}
