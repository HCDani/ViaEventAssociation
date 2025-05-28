using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts;

namespace ViaEventAssociation.Infrastructure.Persistence.Contracts {
    public class EventParticipantsContract : IGetEventParticipants, IDeleteEventParticipant, IStoreEventParticipation {
        protected readonly EFCDbContext context;

        public EventParticipantsContract(EFCDbContext context) {
            this.context = context;
        }

        public async Task CreateAsync(EventParticipation entity) {
            await context.Set<EventParticipation>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public void DeleteParticipant(Guid guestId, Guid eventId) {
            context.Set<EventParticipation>().RemoveRange(context.Set<EventParticipation>().Where(participant => participant.Event.Id == eventId && participant.Guest.Id == guestId));
        }

        public List<EventParticipation> GetParticipants(Guid eventId) {
            return context.Set<EventParticipation>().Where(participant => participant.Event.Id == eventId).ToList();
        }
    }
}
