using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts {
    public interface IGetEventParticipants {
        public List<EventParticipation> GetParticipants(Guid eventId);
    }
}
