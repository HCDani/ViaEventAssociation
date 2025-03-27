using System.Collections.Generic;
using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations
{
    public sealed class Address : ValueObject
    {
        public int PostalCode { get; }
        public string City { get; }
        public string Street { get; }
        public int HouseNumber { get; }

        public Address(int postalCode, string city, string street, int houseNumber)
        {
            PostalCode = postalCode;
            City = city ?? string.Empty;
            Street = street ?? string.Empty;
            HouseNumber = houseNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PostalCode;
            yield return City;
            yield return Street;
            yield return HouseNumber;
        }

        public override string ToString() =>
            $"{Street} {HouseNumber}, {PostalCode} {City}";
    }
}