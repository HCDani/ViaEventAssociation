using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Domain.Common.FakeStuff {
    public interface IUnitOfWork {
        Task SaveChangesASync();
    }
}
