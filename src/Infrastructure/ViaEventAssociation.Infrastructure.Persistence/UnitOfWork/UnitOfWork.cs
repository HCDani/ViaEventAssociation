using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Common.UOWContracts;

namespace ViaEventAssociation.Infrastructure.Persistence.UnitOfWork {
   public class UnitOfWork(EFCDbContext context) : IUnitOfWork {
        public Task SaveChangesASync() {
            return context.SaveChangesAsync();
        }

    }
}
