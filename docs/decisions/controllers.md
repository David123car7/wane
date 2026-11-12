# Controllers

## Decision

The API uses controllers, not Minimal APIs. They live in `src/Wane.Api/Controllers/`, one per resource, and stay thin: they call an Application service and return the result, with no business logic and no EF Core types.

Routes are plain and have no version: `/api/<resource>`, with the resource name in lowercase and plural (for example `/api/profiles`).

## Why

Controllers give structure and conventions out of the box (attribute routing, `[ApiController]` behaviour, filters). The performance difference with Minimal APIs is small and won't show in the benchmarks, since the bottlenecks are storage and the database.

Wane is self-hosted and has no external clients yet, so versioning would add a convention to maintain with nothing to protect. It can be added when the first breaking change appears.

## To Decide

- When API versioning is added, and which style (URL, header or query string).
