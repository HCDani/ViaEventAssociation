using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;

namespace UnitTests.Features.Fake_stuff {
    public interface IEventRepository {
        Task CreateAsync(VEvent vEvent);
        Task<VEvent> GetAsync(Guid eventId);
        Task SaveAsync();
        Task DeleteAsync();
    }
}
