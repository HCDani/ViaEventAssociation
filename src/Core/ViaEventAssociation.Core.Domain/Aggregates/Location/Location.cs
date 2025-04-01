using System;
using System.Net;
using System.Xml.Linq;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.LocationNS {
    public class Location : AggregateRoot<Guid> {
        public LocationName LocationName { get; private set; }
        public MaxCapacity MaxCapacity { get; private set; }
        public Availability Availability { get; private set; }
        public Address Address { get; private set; }

        private Location(Guid id) : base(id) {
            MaxCapacity = MaxCapacity.Default;
        }

        public static Result<Location> Create(Guid id){
            Location location = new Location(id);
            return new Result<Location>(location);
        }

        public Result<LocationName> UpdateName(LocationName newName) {
            if (newName == null) return new Result<LocationName>(101, "Location name is required.");
            LocationName = newName;
            return new Result<LocationName>(LocationName);
        }

        public Result<MaxCapacity> SetMaximumCapacity(MaxCapacity newCapacity) {
            if (newCapacity == null) return new Result<MaxCapacity>(91, "Max capacity is required.");
            MaxCapacity = newCapacity;
            return new Result<MaxCapacity>(MaxCapacity);
        }

        public Result<Availability> SetAvailability(Availability newAvailability) {
            if (newAvailability == null) return new Result<Availability>(84, "Availability is required.");
            Availability = newAvailability;
            return new Result<Availability>(Availability);
        }

        public Result<Address> SetAddress(Address newAddress) { 
            if(newAddress == null) return new Result<Address>(75, "Address is required.");
            Address = newAddress;
            return new Result<Address>(Address);
        }

    }
}
