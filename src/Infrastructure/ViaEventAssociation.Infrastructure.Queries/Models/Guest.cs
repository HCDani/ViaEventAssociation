using System;
using System.Collections.Generic;

namespace ViaEventAssociation.Infrastructure.Queries.Models;

public partial class Guest
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Ppurl { get; set; }

    public virtual ICollection<EventParticipation> EventParticipations { get; set; } = new List<EventParticipation>();
}
