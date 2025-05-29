using System;
using System.Collections.Generic;

namespace ViaEventAssociation.Infrastructure.Queries.Models;

public partial class Location
{
    public Guid Id { get; set; }

    public string? LocationName { get; set; }

    public int? MaxCapacity { get; set; }

    public DateTime? AvailabilityFrom { get; set; }

    public DateTime? AvailabilityTo { get; set; }

    public int? PostalCode { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public int? HouseNumber { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
