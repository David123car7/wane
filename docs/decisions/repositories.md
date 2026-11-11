# Repositories

## Decision

Data access uses specific repositories per aggregate (for example `IPlaylistRepository`), declared in Application and implemented in Infrastructure with EF Core. No generic `IRepository<T>`. EF Core types such as `IQueryable` never cross the interface. Saving goes through a small unit-of-work abstraction. Read methods can return DTOs or projections.

## Why

Application cannot reference Infrastructure, so it needs an abstraction for data. Specific repositories keep EF Core out of Application, make services easy to test with fakes, handle the read-heavy catalog with projections, and allow a caching decorator later for Redis.

## To Decide

- The exact shape of the unit of work.
- When to split read methods into separate query interfaces.
