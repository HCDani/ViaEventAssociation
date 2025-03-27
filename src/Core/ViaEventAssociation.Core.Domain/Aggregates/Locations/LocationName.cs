using System.Collections.Generic;
using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations
{
    public sealed class LocationName : ValueObject
    {
        public string Value { get; }

        public LocationName(string value)
        {
            // Simple constructor. Validate in the domain methods if you want to avoid exceptions.
            Value = value ?? string.Empty;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}