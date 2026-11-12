# Error handling

## Decision

The Application layer reports failures by throwing specific exceptions (not found, conflict and unauthorized, mapped to 404, 409 and 401). One global exception handler in `Wane.Api` catches them and converts each to the right HTTP status code with a `ProblemDetails` response. Unexpected exceptions become a 500 `ProblemDetails`. Controllers stay thin and do not catch exceptions.

Validation errors are not exceptions from the services: invalid input is rejected with a 400 and field errors by data annotations and `[ApiController]` before the service runs (see `validation-and-mapping.md`). Rules that depend on stored data, such as a duplicate profile name, are conflicts. A forbidden exception (403) and a validation exception are not defined, because with a single login with full access there is nothing to forbid, and nothing needs field errors from inside a service.

## Why

It is the simplest option that keeps controllers thin: the happy path stays clean and there is a single place that maps errors to HTTP, using the built-in ASP.NET Core mechanism. It is a solo project with a modest number of failure cases, so a Result type would add more ceremony than it is worth for now.

## Possible change: Result type

If exceptions turn out to hide too much, services could return a Result type (a value or an error) instead of throwing for expected failures, and controllers would map it to HTTP through a helper.
- Pros: failures are explicit in the method signatures and nothing is thrown for expected cases.
- Cons: more boilerplate, a custom type or a library dependency, and a change that touches every service.
- A hybrid is also possible: Result for expected business failures, exceptions only for unexpected problems.

## To Decide

- When to add a forbidden or a validation exception, if a case appears.
- Whether to switch to a Result type, and when.
