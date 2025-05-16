using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Domain.Common.UOWContracts {
    public interface IUnitOfWork {
        public Task SaveChangesASync();
    }
}
