using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    class CreateEventCommandResponse {
        public Guid EventId { get; set; }
        public CreateEventCommandResponse(Guid id) {
            EventId = id;
        }
    }
}
