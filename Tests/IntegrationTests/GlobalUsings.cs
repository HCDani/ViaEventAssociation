﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Core.Tools.OperationResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry;

namespace IntegrationTests {
    public class GlobalUsings {
        public static EFCDbContext CreateDbContext(string? dbname) {
            var optionsBuilder = new DbContextOptionsBuilder<EFCDbContext>();
            optionsBuilder.LogTo(Console.WriteLine);
            if (dbname == null) {
                optionsBuilder.UseSqlite("Data Source=../../viaeventassociation.db");
            } else {
                optionsBuilder.UseSqlite("Data Source="+dbname);
            }
            EFCDbContext context = new (optionsBuilder.Options);
            return context;
        }

        public static void InitializeDatabase(EFCDbContext context) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static T executeCommand<T>(EFCDbContext context,CommandDispatcher cd, Result<T> command) {
            context.ChangeTracker.Clear();
            Assert.IsTrue(command.IsSuccess());
            Task<Result<T>> res = cd.DispatchAsync(command.payLoad);
            res.Wait();
            Result<T> ger = res.Result;
            // Assert
            Assert.IsTrue(ger.IsSuccess());
            return ger.payLoad;
        }

    }
}
