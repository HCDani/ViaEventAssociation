using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Common.Fake_stuff;

namespace UnitTests.Features.Tools.Fakes
{
    class UnitOfWork : IUnitOfWork {
        public Task SaveChangesASync() {
            return Task.CompletedTask;
        }

    }
}
