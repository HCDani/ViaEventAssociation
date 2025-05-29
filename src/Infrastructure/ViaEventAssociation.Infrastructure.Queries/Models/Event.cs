using System;
using System.Collections.Generic;

namespace ViaEventAssociation.Infrastructure.Queries.Models;

public partial class Event
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DurationFrom { get; set; }

    public DateTime? DurationTo { get; set; }

    public int Status { get; set; }

    public int Visibility { get; set; }

    public int? MaxNumberOfGuests { get; set; }

    public Guid? LocationId { get; set; }

    public virtual ICollection<EventParticipation> EventParticipations { get; set; } = new List<EventParticipation>();

    public virtual Location? Location { get; set; }
}
