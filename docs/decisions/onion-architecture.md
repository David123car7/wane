# Onion architecture

## Decision

Wane uses Onion Architecture. Dependencies point inward only:

```
Api -> Application -> Domain
Api -> Infrastructure -> Application
```

- `src/Wane.Domain`: entities, value objects and business rules. No dependencies.
- `src/Wane.Application`: use cases, DTOs, and the interfaces for what it needs from outside (data, storage, cache).
- `src/Wane.Infrastructure`: implements those interfaces (EF Core, storage, cache and so on).
- `src/Wane.Api`: HTTP entry point and composition root (dependency injection wiring).

Rules:
- Domain never references another project. Application never references Infrastructure.
- Define an interface in Application first, then implement it in Infrastructure.
- No raw SQL: use EF Core. Keep mapping in Fluent API configurations, not attributes on Domain entities.

See `docs/architecture.md` for the full explanation.

## Why

The business logic stays at the centre, independent of the database, framework and external services. The inner layers can be tested without infrastructure, and technical details can be replaced without touching them.

## To Decide

- Whether architecture tests should enforce the dependency rule.
