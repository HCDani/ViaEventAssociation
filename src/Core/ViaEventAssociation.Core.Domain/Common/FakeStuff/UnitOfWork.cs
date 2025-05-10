using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Domain.Common.FakeStuff {
   public class UnitOfWork : IUnitOfWork {
        public Task SaveChangesASync() {
            return Task.CompletedTask;
        }

    }
}
