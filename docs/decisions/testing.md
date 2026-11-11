# Testing

## Decision

- Framework: xUnit. Assertions: xUnit's built-in ones (Shouldly is an option).
- Domain: plain unit tests of entities and invariants, no database.
- Application: unit tests of services with fake repositories, no database.
- Infrastructure: repository tests against a real PostgreSQL, started with Testcontainers.
- Api: integration tests of the full request flow (auth, validation, error mapping, status codes) with `WebApplicationFactory` against the same Testcontainers database.
- Test projects live in `tests/`: `Wane.Domain.Tests`, `Wane.Application.Tests` and `Wane.IntegrationTests` (repositories and API together).
- Tests run in CI on every pull request with `dotnet build` and `dotnet test`. The workflow is added once there are tests to run.

## Why

The Onion architecture lets the inner layers be tested without infrastructure. A real PostgreSQL is used for the layers that touch data, because stand-ins behave differently (constraints, GUID handling, SQL translation) and tests can pass while production fails. With specific repositories, simple in-memory fakes are easy to write and reuse.

## Possible change: other tools

- NUnit or MSTest instead of xUnit. There is no strong reason to switch, xUnit has the best ecosystem around ASP.NET Core.
- EF Core in-memory provider or SQLite instead of PostgreSQL. Faster and with no Docker needed, but they do not behave like PostgreSQL.
- A mocking library such as NSubstitute instead of, or alongside, hand-written fakes.
- FluentAssertions instead of the built-in assertions. Recent versions need a commercial license.
- Splitting `Wane.IntegrationTests` into one project per layer if it grows.

## To Decide

- Fakes only, or also a mocking library.
- Whether the CI test job becomes a required check in the ruleset.
- Whether architecture tests enforce the dependency rule (see `onion-architecture.md`).
