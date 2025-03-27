using System;
using System.Collections.Generic;
using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations
{
    public sealed class Availability : ValueObject
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public Availability(DateTime from, DateTime to)
        {
            // You might decide whether it’s invalid to have from >= to. 
            // Typically, domain checks can be done in the domain methods.

            From = from;
            To = to;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }

        public override string ToString() => $"From {From} to {To}";
    }
}