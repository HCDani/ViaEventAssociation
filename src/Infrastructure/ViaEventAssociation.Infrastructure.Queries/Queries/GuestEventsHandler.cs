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
    public class GuestEventsHandler(ScaffoldingDbinitContext ctx) : IQueryHandler<GuestEvents.Query, GuestEvents.Answer> {
        public async Task<GuestEvents.Answer> HandleAsync(GuestEvents.Query query) {
            List<GuestEvents.EventInfo> Events = await ctx.EventParticipations
                .Where(ep => ep.GuestId == query.GuestId)
                .Select(ep => new GuestEvents.EventInfo(
                    ep.Event.Id,
                    ep.Event.Title,
                    ep.Event.Description,
                    ep.Event.DurationFrom,
                    ep.Event.DurationTo,
                    ep.Event.MaxNumberOfGuests,
                    ep.Event.EventParticipations.Where(ep => ep.ParticipationStatus==0).Count()))
                .ToListAsync();
            return new GuestEvents.Answer(Events);
        }
    }
}
