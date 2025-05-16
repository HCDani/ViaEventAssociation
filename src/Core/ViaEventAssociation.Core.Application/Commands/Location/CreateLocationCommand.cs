using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Application.Commands.GuestNS;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using System.Numerics;

namespace ViaEventAssociation.Core.Application.Commands.LocationNS {
    public class CreateLocationCommand {
        public Address Address { get; }
        public Availability Availability { get; }
        public LocationName LocationName { get; }
        public MaxCapacity MaxCapacity { get; }

        private CreateLocationCommand(Address address, Availability availability, LocationName locationName, MaxCapacity maxCapacity) {
            Address = address;
            Availability = availability;
            LocationName = locationName;
            MaxCapacity = maxCapacity;
        }
        public static Result<CreateLocationCommand> Create(string postalcode, string city, string street, string housenumber, string availabilityFrom, string availabilityTo, string locationName, string maxCapacity) {
            Result<Address> addressResult = Address.Create(int.Parse(postalcode), city, street, int.Parse(housenumber));
            Result<Availability> availabilityResult = Availability.Create(DateTime.Parse(availabilityFrom), DateTime.Parse(availabilityTo));
            Result<LocationName> locationNameResult = LocationName.Create(locationName);
            Result<MaxCapacity> maxCapacityResult = MaxCapacity.Create(int.Parse(maxCapacity));
            return Result<CreateLocationCommand>.CombineResultsInto(addressResult, availabilityResult, locationNameResult, maxCapacityResult)
                .WithPayLoadIfSuccess(() => new CreateLocationCommand(addressResult.payLoad, availabilityResult.payLoad, locationNameResult.payLoad, maxCapacityResult.payLoad));
        }

        public Guid LocationId { get; set; }
        public Result<CreateLocationCommand> AddResponse(Result<Location> location) {
            if (location.resultCode != 0) {
                return new Result<CreateLocationCommand>(location.resultCode, location.errorMessage);
            }
            LocationId = location.payLoad.Id;
            return new Result<CreateLocationCommand>(this);
        }
    }
}
