using IntegrationTests.RepositoryTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Infrastructure.Queries.Persistence;

namespace IntegrationTests {
    internal class VeaWebApplicationFactory : WebApplicationFactory<Program> {
        private IServiceCollection serviceCollection;
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            // setup extra test services.
            builder.ConfigureTestServices(services => {
                serviceCollection = services;
                // Remove the existing DbContexts and Options
                services.RemoveAll(typeof(DbContextOptions<EFCDbContext>));
                services.RemoveAll(typeof(DbContextOptions<ScaffoldingDbinitContext>));
                services.RemoveAll<EFCDbContext>();
                services.RemoveAll<ScaffoldingDbinitContext>();

                string connString = "Data Source=../../endtoendtest.db";
                services.AddDbContext<EFCDbContext>(options => {
                    options.UseSqlite(connString);
                });
                services.AddDbContext<ScaffoldingDbinitContext>(options => {
                    options.UseSqlite(connString);
                });
                ScaffoldingDbinitContext dbContext = services.BuildServiceProvider().GetService<ScaffoldingDbinitContext>()!;
                JSONLoader.SeedDatabaseFromJson(dbContext).Wait();
            });
        }
    }
}
