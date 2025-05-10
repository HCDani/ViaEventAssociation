using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;

namespace ViaEventAssociation.Core.Domain.Common.FakeStuff {
    public class InMemEventRepoStub : IEventRepository {
        public Dictionary<Guid, VEvent> events = new Dictionary<Guid, VEvent>();

        public Task CreateAsync(VEvent vEvent) {
            events.Add(vEvent.Id, vEvent);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid eventId) {
            if (events.ContainsKey(eventId)) {
                events.Remove(eventId);
            }
            return Task.CompletedTask;
        }

        public Task<VEvent> GetAsync(Guid eventId) {
            return Task.FromResult(events[eventId]);
        }

        public Task SaveAsync() {
            return Task.CompletedTask;
        }
    }
}
