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
        public static Result<CreateLocationCommand> Create(string address, string availability, string locationName, int? maxCapicity) {
            Result<Address> addressResult = Address.Create(address);
            Enum.TryParse<Availability>(availability, out Availability availabilityResult);
            Result<LocationName> locationNameResult = LocationName.Create(locationName);
            Result<MaxCapacity> maxCapacityResult = MaxCapacity.Create(maxCapicity);
            return Result<CreateLocationCommand>.CombineResultsInto(addressResult, availabilityResult, locationNameResult, maxCapacityResult )
                .WithPayLoadIfSuccess(() => new CreateLocationCommand(addressResult.payLoad, availabilityResult, locationNameResult.payLoad,maxCapacityResult.payLoad ));
        }

        public Guid GuestId { get; set; }
        public Result<CreateLocationCommand> AddResponse(Result<Location> location) {
            if (location.resultCode != 0) {
                return new Result<CreateLocationCommand>(location.resultCode, location.errorMessage);
            }
            GuestId = location.payLoad.Id;
            return new Result<CreateLocationCommand>(this);
        }
    }
}
