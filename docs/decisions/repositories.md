# Repositories

## Decision

Data access uses specific repositories per aggregate (for example `IPlaylistRepository`), declared in Application and implemented in Infrastructure with EF Core. No generic `IRepository<T>`. EF Core types such as `IQueryable` never cross the interface. Read methods can return DTOs or projections.

Repositories never save. Their methods only record changes: `Add` for new entities and `Remove` for deletions. There is no `Update` method. To change an existing entity, the service gets it from the repository, calls a method on the entity (see `validation-and-mapping.md`), and saves. The entity is tracked by the `DbContext`, so EF Core finds the change and writes only the columns that changed. For this to work, reads used for updates must return the entity itself, tracked, not a projection or a no-tracking query. Reads that only produce DTOs can be untracked. Saving goes through a small unit-of-work abstraction, declared in Application and implemented in Infrastructure on top of the same `DbContext` the repositories use. The Application service calls it once, at the end of the use case, after all the rules and checks have passed. EF Core writes everything in that single call as one transaction, so a use case that changes several things (for example a track and its file) either saves all of them or none.

The unit of work starts with a single save method. Explicit transactions (begin, commit, roll back) are added only when a use case needs more than one save, or mixes in work outside the database.

## Why

Application cannot reference Infrastructure, so it needs an abstraction for data. Specific repositories keep EF Core out of Application, make services easy to test with fakes, handle the read-heavy catalog with projections, and allow a caching decorator later for Redis.

The service owns the use case boundary. A repository cannot know whether its call is the whole operation or one step of a larger one, so it must not decide when to save. Because nothing is written until the single save, there is no state where one change was stored and the next failed. In tests, the fake unit of work can record whether save was called, which checks that a failed rule saves nothing.

Leaving out `Update` keeps the repository small and avoids marking every column as modified. The cost is a rule that is not visible in the code: a read that returns a tracked entity. Each update use case should have a test that checks the change is saved.

## To Decide

- When a use case needs explicit transactions, and how the unit of work exposes them.
- When to split read methods into separate query interfaces.
- Whether to add an explicit `Update` method that does nothing in the EF Core implementation, if the tracked-entity rule proves easy to break.
