using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;

namespace ViaEventAssociation.Infrastructure.Persistence.Repositories {
    public class LocationRepository : GenericRepository<Location>, ILocationRepository {
        public LocationRepository(EFCDbContext context) : base(context) {
        }
    }
}
