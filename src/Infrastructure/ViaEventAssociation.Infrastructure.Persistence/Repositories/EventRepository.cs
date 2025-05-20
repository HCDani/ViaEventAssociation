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
         public async new Task<VEvent> GetAsync(Guid id) {
            VEvent vevent = await context.Set<VEvent>().FindAsync(id);
            await context.Entry(vevent).Reference(b => b.Location).LoadAsync();
            return vevent;
        }
    }
}
