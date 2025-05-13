using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;

namespace ViaEventAssociation.Core.Domain.Common.RepoContracts {
    public interface ILocationRepository {
        Task CreateAsync(Location location);
        Task<Location> GetAsync(Guid locationId);
        Task DeleteAsync(Guid locationId);
    }
}
