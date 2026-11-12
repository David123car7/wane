# Application layer

## Decision

`Wane.Application` is organized by feature, with a `Common` folder for what is shared across features:

```
Wane.Application/
├── Common/
│   ├── Abstractions/    IUnitOfWork
│   └── Exceptions/      NotFound, Conflict, Unauthorized
├── Profiles/
│   ├── Dtos/            request and response DTOs, and their mapping extension methods
│   ├── IProfileRepository
│   └── ProfileService
├── Playlists/           same shape
├── Tracks/              same shape
└── DependencyInjection  AddApplication, called by Wane.Host
```

- Each feature holds its own DTOs, its repository interface and its service. Repository interfaces are per aggregate (see `repositories.md`): Profile, Track and Playlist. `PlaylistTrack` and `TrackFile` are reached through their parents and have no repository of their own.
- Services are concrete classes, with no interface. A service gets an interface only when something needs one.
- `Common` holds the unit of work (see `repositories.md`) and the exceptions that the global handler in `Wane.Api` maps to HTTP (see `error-handling.md`).
- `AddApplication` registers the services and is called by `Wane.Host`, the same way as `AddInfrastructure`.
- Application references Domain only. It has no reference to EF Core or ASP.NET Core. The one exception is the dependency injection abstractions package, needed by `AddApplication`.
- Folders are created when their feature is built, not in advance.

## Why

Organizing by feature keeps everything about a feature in one place, so adding one touches one folder, not one folder per kind of file. Concrete services are enough because they are tested directly with fake repositories (see `testing.md`) and the controllers can depend on the class itself. An interface would add a file per service with nothing to gain.

## To Decide

- When a service needs an interface (for example to fake it in controller tests).
- When a feature grows enough to split its service into several, or into separate commands and queries.
