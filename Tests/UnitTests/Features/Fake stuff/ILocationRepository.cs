using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;

namespace UnitTests.Features.Fake_stuff {
    interface ILocationRepository {
        Task CreateAsync(Location location);
        Task<Location> GetAsync(Guid locationId);
        Task SaveAsync();
        Task DeleteAsync();
    }
}
