using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Infrastructure.Persistence;

namespace UnitTests.Features.Tools.Fakes {
    public class InMemGenericRepoStub<T>() : IGenericRepository<T> where T : Entity<Guid> {
        public Dictionary<Guid, T> table { get; } = new Dictionary<Guid, T>();
        public async Task CreateAsync(T entity) {
            table.Add(entity.Id, entity);
        }
        public async Task<T> GetAsync(Guid id) {
            return table[id];
        }

        public async Task DeleteAsync(Guid id) {
            if (table.ContainsKey(id)) {
                table.Remove(id);
            }
        }
    }
}
