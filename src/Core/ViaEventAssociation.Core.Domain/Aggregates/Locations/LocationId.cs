using System;
using System.Collections.Generic;
using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations
{
    public sealed class LocationId : ValueObject
    {
        public Guid Value { get; }

        public LocationId(Guid value)
        {
            // You could optionally validate that the Guid is not Guid.Empty
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}