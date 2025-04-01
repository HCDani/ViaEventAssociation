using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values {
    public record Address {
        public int PostalCode { get; }
        public string City { get; }
        public string Street { get; }
        public int HouseNumber { get; }

        private Address(int postalCode, string city, string street, int houseNumber) {
            PostalCode = postalCode;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
        }
        public static Result<Address> Create(int? postalCode, string? city, string? street, int? houseNumber) {
            return Validate(postalCode, city, street, houseNumber) ?? new Result<Address>(new Address((int)postalCode,(string)city,(string)street,(int)houseNumber));
        }
        private static Result<Address>? Validate(int? postalCode, string? city, string? street, int? houseNumber) {
            if (postalCode == null || postalCode < 1000 || postalCode > 9999) return new Result<Address>(71, "Invalid Postal Code.");
            if (string.IsNullOrEmpty(city)) return new Result<Address>(72, "City cannot be empty.");
            if (string.IsNullOrEmpty(street)) return new Result<Address>(73, "Street cannot be empty.");
            if (houseNumber == null || houseNumber <= 0) return new Result<Address>(74, "House number has to be bigger than 0.");
            return null;
        }
    }
}