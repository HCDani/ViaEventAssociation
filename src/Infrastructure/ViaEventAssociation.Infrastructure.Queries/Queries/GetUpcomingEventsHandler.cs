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
    public class GetUpcomingEventsHandler(ScaffoldingDbinitContext ctx) : IQueryHandler<UpcomingEvents.Query, UpcomingEvents.Answer> {
        public async Task<UpcomingEvents.Answer> HandleAsync(UpcomingEvents.Query query) {
            List<UpcomingEvents.UpEvent> upEvents = await ctx.Events
                .Where(e => e.Id == query.EventId && e.DurationFrom > DateTime.Now)
                .Select(e => new UpcomingEvents.UpEvent(
                    e.Id,
                    e.Title,
                    e.Description,
                    e.DurationFrom,
                    e.DurationTo,
                    e.MaxNumberOfGuests,
                    e.EventParticipations.Where(ep => ep.ParticipationStatus == 0).Count(),
                    (int)e.Visibility))
                .ToListAsync();
            return new UpcomingEvents.Answer(upEvents);
        }
    }
}
