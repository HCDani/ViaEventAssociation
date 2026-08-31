# VIA Event Association

An event-handling system for campus events at VIA University College. Staff create
events at locations, invite or admit guests, and the domain enforces the rules: an
event cannot be published until it is fully described, it cannot take more guests than
its location holds, and its status and visibility can only move in legal directions.

Built solo as the Domain Centric Architecture project, 6th semester.

## Architecture

Onion architecture on .NET 8, with dependencies pointing inwards only. The domain has
no reference to persistence, the web layer, or any framework.

**Core** holds the domain and the application layer. `VEvent`, `Guest` and `Location`
are aggregates; `EventParticipation` is the entity joining a guest to an event. Every
attribute is a value object — `EventTitle`, `EventDescription`, `EventDuration`,
`MaxNumberOfGuests`, `Email`, `Address` — each responsible for validating itself, so an
invalid value cannot exist in the first place.

**Infrastructure** implements persistence with EF Core over SQLite, and holds the read
side's query handlers.

**Presentation** is an ASP.NET Core Web API with Swagger, one controller per endpoint
in the REPR style, so each request, endpoint and response sits in a single file rather
than in a fat controller.

### Reads and writes are separated

The write side takes a command, dispatches it through `ICommandDispatcher` to a handler,
loads the aggregate, calls a domain method and saves through a repository. Commands
carry no behaviour and handlers carry no rules.

The read side skips the domain entirely. `IQueryDispatcher` sends a query to a handler
in `Infrastructure.Queries` that projects straight from the database into a contract in
`Core.QueryContracts` — `EventInfo`, `GuestInfo`, `GuestEvents`, `UpcomingEvents`. This
avoids loading aggregates just to display them.

### Two decisions worth calling out

**Failures are values, not exceptions.** Every operation that can fail returns
`OperationResult`, so a caller has to deal with the failure to get at the value. Domain
rule violations are expected outcomes and are not thrown.

**Time is injected.** `ISystemTime` abstracts the clock, and the unit tests substitute
`FakeSystemTime`. Rules like "an event cannot start in the past" are then testable
without waiting for or faking real time.

## Layout

```
src/Core/
  ViaEventAssociation.Core.Domain            Aggregates, entities, value objects
  ViaEventAssociation.Core.Application        Commands, handlers, command dispatcher
  ViaEventAssociation.Core.QueryContracts     Read-model shapes
  ViaEventAssociation.Core.QueryApplication   Query dispatcher
  ViaEventAssociation.Core.Tools              OperationResult, SystemTime
src/Infrastructure/
  ...Persistence                              EF Core context, repositories
  ...Queries                                  Read-side query handlers
src/Presentation/
  ...WebAPI                                   Controllers, Swagger
Tests/
  UnitTests                                   Domain and handler behaviour
  IntegrationTests                            Repositories, queries and the API
Documentation/
```

## Running it

```bash
dotnet run --project src/Presentation/ViaEventAssociation.Presentation.WebAPI
```

The SQLite database is created on startup, and Swagger UI is served in development.

## Tests

```bash
dotnet test
```

Unit tests cover the domain rules per feature — updating an event's title, description,
duration, maximum guests, status and visibility, and guest participation — using
in-memory stubs and a fake clock, with commands and handlers tested separately from the
aggregates. Integration tests run the repositories and query handlers against a real
SQLite database and exercise the API through a `WebApplicationFactory`.
