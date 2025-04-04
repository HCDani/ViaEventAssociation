using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Features.Fake_stuff {
    public interface IUnitOfWork {
        Task SaveChangesASync();
    }
}
