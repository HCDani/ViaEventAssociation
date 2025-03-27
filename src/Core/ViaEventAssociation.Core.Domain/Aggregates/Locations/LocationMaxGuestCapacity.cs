using System.Collections.Generic;
using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations
{
    public sealed class LocationMaxGuestCapacity : ValueObject
    {
        public int Value { get; }

        public LocationMaxGuestCapacity(int value)
        {
            // For demonstration we simply store the value directly.
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}