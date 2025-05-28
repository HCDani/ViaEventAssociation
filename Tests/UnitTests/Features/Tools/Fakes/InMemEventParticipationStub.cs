using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts;

namespace UnitTests.Features.Tools.Fakes {
    public class InMemEventParticipationStub : IGetEventParticipants, IDeleteEventParticipant, IStoreEventParticipation {
        List<EventParticipation> eventGuestParticipations = new List<EventParticipation>();
        public InMemEventParticipationStub() {
        }
        public Task CreateAsync(EventParticipation entity) {
            eventGuestParticipations.Add(entity);
            return Task.CompletedTask;
        }

        public void DeleteParticipant(Guid guestId, Guid eventId) {
            eventGuestParticipations.RemoveAll(ep => ep.Guest.Id == guestId && ep.Event.Id == eventId);
        }

        public List<EventParticipation> GetParticipants(Guid eventId) {
            return eventGuestParticipations.Where(ep => ep.Event.Id == eventId).ToList();
        }
        public void Clear() {
            eventGuestParticipations.Clear();
        }
    }
}
