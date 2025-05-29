using System;
using System.Collections.Generic;

namespace ViaEventAssociation.Infrastructure.Queries.Models;

public partial class EventParticipation
{
    public Guid Id { get; set; }

    public int ParticipationStatus { get; set; }

    public Guid GuestId { get; set; }

    public Guid EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Guest Guest { get; set; } = null!;
}
