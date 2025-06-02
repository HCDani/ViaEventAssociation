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
    public class GetUnpublishedEventsHandler(ScaffoldingDbinitContext ctx) : IQueryHandler<UnpublishedEvents.Query, UnpublishedEvents.Answer> {
        public async Task<UnpublishedEvents.Answer> HandleAsync(UnpublishedEvents.Query query) {
            List<UnpublishedEvents.uEvent> uEvents = await ctx.Events
                .Where(e => e.Status != 3)
                .Select(e => new UnpublishedEvents.uEvent(
                    e.Id,
                    e.Title,
                    e.Status))
                .ToListAsync();
            return new UnpublishedEvents.Answer(uEvents);
        }
    }
}
