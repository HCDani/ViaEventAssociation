using System.Collections.Generic;
using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations
{
    public sealed class MaxCapacity : ValueObject
    {
        public int Value { get; }

        public MaxCapacity(int value)
        {
            // In real usage, you might store negative as well, 
            // but we typically expect a domain check to disallow negative capacities.
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}