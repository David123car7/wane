# Controllers

## Decision

The API uses controllers, not Minimal APIs. They live in `src/Wane.Api/Controllers/`, one per resource, and stay thin: they call an Application service and return the result, with no business logic and no EF Core types.

## Why

Controllers give structure and conventions out of the box (attribute routing, `[ApiController]` behaviour, filters). The performance difference with Minimal APIs is small and won't show in the benchmarks, since the bottlenecks are storage and the database.

## To Decide

- Route naming and API versioning.
