using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;

namespace ViaEventAssociation.Infrastructure.Persistence.Repositories {
    public class EventRepository : GenericRepository<VEvent> , IEventRepository{
        public EventRepository(EFCDbContext context) : base(context) {
        }
    }
}
