using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Infrastructure.Persistence.Repositories;

namespace UnitTests.Features.Tools.Fakes {
    public class InMemGuestRepoStub : InMemGenericRepoStub<Guest> , IGuestRepository {
    }
}
