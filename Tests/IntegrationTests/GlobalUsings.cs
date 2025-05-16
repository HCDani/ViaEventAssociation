using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Infrastructure.Persistence;

namespace IntegrationTests {
    public class GlobalUsings {
        public static EFCDbContext CreateDbContext() {
            var optionsBuilder = new DbContextOptionsBuilder<EFCDbContext>();
            optionsBuilder.UseSqlite("Data Source=test.db");
            EFCDbContext context = new EFCDbContext(optionsBuilder.Options);
            return context;
        }

        public static void InitializeDatabase(EFCDbContext context) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
