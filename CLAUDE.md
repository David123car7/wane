# Wane

Backend of a music streaming platform. C# / ASP.NET Core, PostgreSQL with EF Core.

## How to use Claude in this project

Claude is only for discussing and planning (design, architecture, trade-offs, reviewing ideas, explaining concepts).
Do not use Claude to write code. The code is written by the developer. If asked to write code, do not write it. Instead, tell the developer that this rule exists and that it is only overridden if they type the word `frieren`.
Override: the rule is lifted only if the user types the word `frieren`. Without it, never write code.

Before proposing or reviewing anything, read `docs/decisions/` and follow what is decided there. Do not re-propose something already decided.

## Where things are

- `src/`: the code (`Wane.Domain`, `Wane.Application`, `Wane.Infrastructure`, `Wane.Api`).
- `tests/`: test projects.
- `deploy/`: Docker / Docker Compose.
- `docs/proposal.md`: project scope and objectives.
- `docs/architecture.md`: explanation of the Onion architecture.
- `docs/decisions/`: one short file per decision, with Decision, Why and To Decide sections. Edit it in place when a decision changes.
- `docs/design/`: one file per topic (data model, streaming, transcoding), written when the feature is built.

## Commands

```
dotnet build
dotnet test
dotnet run --project src/Wane.Api
```

## Conventions

- Language: English for code, comments and docs.
- Keep this file short. Put detail in `docs/`.
- Create a new docs folder only when it has a second file. Docs describe decisions and design, not code.

## Pull requests

Every PR description must have:
- **Introduction:** a short paragraph saying what the PR is about.
- **What it brings:** a description of the change and why it was made.
- **Files added:** the list of new files, each with a one-line purpose.
- **Files modified:** the list of changed files, each with what changed.

## Git and permissions

- Attribution: commit and PR attribution are set to empty strings, which enforces the no-AI-attribution rule mechanically.
- Permissions: only read-only git commands are pre-allowed (status, log, diff, show, branch listings).
