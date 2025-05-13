using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;
using ViaEventAssociation.Infrastructure.Persistence;

namespace UnitTests.Features.Tools.Fakes {
   public class FUnitOfWork() : IUnitOfWork {
        public Task SaveChangesASync() {
            return Task.CompletedTask;
        }

    }
}
