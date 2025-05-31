using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Infrastructure.Queries.Persistence {
    public class ScaffoldingDbContextFactory {
        public static ScaffoldingDbinitContext CreateDbContext(string? dbname) {
            var optionsBuilder = new DbContextOptionsBuilder<ScaffoldingDbinitContext>();
            optionsBuilder.LogTo(Console.WriteLine);
            if (dbname == null) {
                optionsBuilder.UseSqlite("Data Source=../../viaeventassociation.db");
            } else {
                optionsBuilder.UseSqlite("Data Source=" + dbname);
            }
            ScaffoldingDbinitContext context = new(optionsBuilder.Options);
            return context;
        }
        public static void InitializeDatabase(ScaffoldingDbinitContext context) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
