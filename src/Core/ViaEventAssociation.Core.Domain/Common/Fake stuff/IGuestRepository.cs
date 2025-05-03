using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;

namespace ViaEventAssociation.Core.Domain.Common.Fake_stuff {
    public interface IGuestRepository {
        Task CreateAsync(Guest guest);
        Task<Guest> GetAsync(Guid guestId);
        Task SaveAsync();
        Task DeleteAsync();
    }
}
