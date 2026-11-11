# Onion architecture

## Decision

Wane uses Onion Architecture. Dependencies point inward only:

```
Host -> Api, Application, Infrastructure
Api -> Application -> Domain
Infrastructure -> Application, Domain
```

- `src/Wane.Domain`: entities, value objects and business rules. No dependencies.
- `src/Wane.Application`: use cases, DTOs, and the interfaces for what it needs from outside (data, storage, cache).
- `src/Wane.Infrastructure`: implements those interfaces (EF Core, storage, cache and so on).
- `src/Wane.Api`: the HTTP layer (controllers, exception handler). A class library, not an executable. It does not reference Infrastructure.
- `src/Wane.Host`: the executable and composition root. It loads the configuration, wires each layer through its registration method (for example `AddInfrastructure`) and runs the web server.

Rules:
- Domain never references another project. Application never references Infrastructure. Api never references Infrastructure.
- Only Host references every layer.
- Define an interface in Application first, then implement it in Infrastructure.
- No raw SQL: use EF Core. Keep mapping in Fluent API configurations, not attributes on Domain entities.

See `docs/architecture.md` for the full explanation.

## Why

The business logic stays at the centre, independent of the database, framework and external services. The inner layers can be tested without infrastructure, and technical details can be replaced without touching them.

A separate Host keeps Api as pure HTTP presentation: the compiler stops it from reaching Infrastructure. It also gives one place to wire everything, which a second executable (for example a transcoding worker) can reuse through the same registration methods.

## To Decide

- Whether architecture tests should enforce the dependency rule.
