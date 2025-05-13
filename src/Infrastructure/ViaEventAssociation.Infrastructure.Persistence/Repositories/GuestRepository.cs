using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;

namespace ViaEventAssociation.Infrastructure.Persistence.Repositories {
    public class GuestRepository : GenericRepository<Guest>, IGuestRepository {
        public GuestRepository(EFCDbContext context) : base(context) {
        }
    }
}
