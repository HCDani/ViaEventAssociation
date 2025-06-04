using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.SystemTime;
using ViaEventAssociation.Infrastructure.Queries.Persistence;

namespace ViaEventAssociation.Infrastructure.Queries.Queries {
    public class GetGuestInfoHandler(ScaffoldingDbinitContext ctx) : IQueryHandler<GuestInfo.Query, GuestInfo.Answer> {
        public async Task<GuestInfo.Answer> HandleAsync(GuestInfo.Query query) {
            GuestInfo.Answer? guest = await ctx.Guests
                .Where(g => g.Id == query.GuestId)
                .Select(g => new GuestInfo.Answer(
                    g.Id,
                    g.FirstName,
                    g.LastName,
                    g.Email,
                    g.Ppurl,
                    g.EventParticipations.Count(ep => ep.ParticipationStatus == 1), // Count of invitations
                    g.EventParticipations.Count(ep => ep.ParticipationStatus == 0 && ep.Event.DurationFrom > SystemTimeHolder.SystemTime.GetCurrentDateTime()) // Count of upcoming events
                ))
                .FirstOrDefaultAsync();
            if (guest == null) {
                throw new KeyNotFoundException($"Guest with ID {query.GuestId} not found.");
            }
            return guest;
        }
    }
}
