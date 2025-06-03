using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.QueryApplication.QueryDispatching;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Infrastructure.Queries.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EFCDbContext>(options =>
    options.UseSqlite("Data Source=../../viaeventassociation.db"));

builder.Services.AddDbContext<ScaffoldingDbinitContext>(options =>
    options.UseSqlite("Data Source=../../viaeventassociation.db"));

builder.Services.AddScoped<ICommandDispatcher,CommandDispatcher>();
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EFCDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
