# Error handling

## Decision

The Application layer reports failures by throwing specific exceptions (for example not found, conflict, unauthorized). One global exception handler in `Wane.Api` catches them and converts each to the right HTTP status code with a `ProblemDetails` response. Unexpected exceptions become a 500 `ProblemDetails`. Controllers stay thin and do not catch exceptions.

## Why

It is the simplest option that keeps controllers thin: the happy path stays clean and there is a single place that maps errors to HTTP, using the built-in ASP.NET Core mechanism. It is a solo project with a modest number of failure cases, so a Result type would add more ceremony than it is worth for now.

## Possible change: Result type

If exceptions turn out to hide too much, services could return a Result type (a value or an error) instead of throwing for expected failures, and controllers would map it to HTTP through a helper.
- Pros: failures are explicit in the method signatures and nothing is thrown for expected cases.
- Cons: more boilerplate, a custom type or a library dependency, and a change that touches every service.
- A hybrid is also possible: Result for expected business failures, exceptions only for unexpected problems.

## To Decide

- The exact set of Application exceptions.
- How validation errors are reported (400 with field errors), together with the validation decision.
- Whether to switch to a Result type, and when.
