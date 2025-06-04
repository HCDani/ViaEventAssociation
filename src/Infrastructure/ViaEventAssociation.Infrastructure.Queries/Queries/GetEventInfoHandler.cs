using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.Queries.Persistence;

namespace ViaEventAssociation.Infrastructure.Queries.Queries {
    public class GetEventInfoHandler(ScaffoldingDbinitContext ctx) : IQueryHandler<EventInfo.Query, EventInfo.Answer> {
        public async Task<EventInfo.Answer> HandleAsync(EventInfo.Query query) {
            EventInfo.Answer? eventDetails = await ctx.Events
                .Where(e => e.Id == query.EventId)
                .Select(e => new EventInfo.Answer(
                    e.Id,
                    e.Title,
                    e.Description,
                    e.DurationFrom,
                    e.DurationTo,
                    e.MaxNumberOfGuests,
                    e.EventParticipations.Count(ep => ep.ParticipationStatus == 0),
                    e.Location != null ? e.Location.LocationName : "N/A", // Fix for CS8602
                    e.Visibility,
                    e.EventParticipations.Select(g => new EventInfo.GuestDetails(g.Guest.Id, g.Guest.FirstName, g.Guest.LastName, g.Guest.Ppurl)).ToList()
                ))
                .FirstOrDefaultAsync();
            if (eventDetails == null) {
                throw new KeyNotFoundException($"Event with ID {query.EventId} not found.");
            }
            return eventDetails;
        }
    }
}
