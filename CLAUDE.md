# Wane

Backend of a music streaming platform (Master's project, IPCA). C# / ASP.NET Core, PostgreSQL with EF Core.
Read `docs/proposal.md` for scope and `docs/architecture.md` for the architecture.

## How to use Claude in this project

Claude is only for discussing and planning (design, architecture, trade-offs, reviewing ideas, explaining concepts).
Do not use Claude to write code. The code is written by the developer. If asked to write code, do not write it. Instead, tell the developer that this rule exists and that it is only overridden if they type the word `frieren`.
Override: the rule is lifted only if the user types the word `frieren`. Without it, never write code.

## Architecture: Onion

Dependencies point inward only:

```
Api -> Application -> Domain
Api -> Infrastructure -> Application
```

- `src/Wane.Domain`: entities, value objects, business rules. No dependencies.
- `src/Wane.Application`: use cases, DTOs, and the interfaces for what it needs from outside (data, storage, cache).
- `src/Wane.Infrastructure`: implements those interfaces (EF Core, storage, cache, etc.).
- `src/Wane.Api`: HTTP entry point and composition root (dependency injection wiring).
- `tests/`: test projects, added as the code grows.
- `deploy/`: Docker / Docker Compose.

Rules:
- Domain never references another project. Application never references Infrastructure.
- Define an interface in Application first, then implement it in Infrastructure.
- No raw SQL: use EF Core. Keep mapping in Fluent API configurations, not attributes on Domain entities.

## Commands

```
dotnet build
dotnet test
dotnet run --project src/Wane.Api
```

## Docs

- `docs/proposal.md`: project scope and objectives (stable).
- `docs/architecture.md`: the Onion layers and their rules.
- `docs/decisions/`: one short file per decision (ADR), numbered, e.g. `0001-name.md`. Check here before proposing something already decided.
- `docs/design/`: one file per topic (data model, streaming, transcoding), written when the feature is built.
- Create a new docs folder only when it has a second file. Docs describe decisions and design, not code.

## Conventions

- Language: English for code, comments and docs.
- Keep this file short. Put detail in `docs/`.

## Pull requests

Every PR description must have:
- **Introduction:** a short paragraph saying what the PR is about.
- **What it brings:** a description of the change and why it was made.
- **Files added:** the list of new files, each with a one-line purpose.
- **Files modified:** the list of changed files, each with what changed.

## Git and permissions

- Attribution: commit and PR attribution are set to empty strings, which enforces the no-AI-attribution rule mechanically.
- Permissions: only read-only git commands are pre-allowed (status, log, diff, show, branch listings).
