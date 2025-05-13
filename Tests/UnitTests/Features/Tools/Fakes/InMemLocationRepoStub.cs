using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Infrastructure.Persistence.Repositories;

namespace UnitTests.Features.Tools.Fakes {
    public class InMemLocationRepoStub : GenericRepository<Location>, ILocationRepository {
        public InMemLocationRepoStub(EFCDbContext context) : base(context) {
        }
    }
}
