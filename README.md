# Wane

Backend of a self-hosted music streaming platform, built with C# / ASP.NET Core.

You deploy your own Wane instance and use it to listen to your own music. The instance is protected by a single login, and inside it you create profiles. Each profile has its own playlists, favourites and listening history, and all profiles share the same music library.

> **Status:** early development. The project structure and the first decisions are defined, and there are no features yet.

## Planned features

- Music catalog: artists, albums, tracks, playlists and search
- Profiles with their own playlists, favourites and listening history
- Audio upload, background transcoding and storage
- Streaming with HTTP range requests (seeking) and adaptive streaming (HLS)
- Caching, and load tests to measure how the system behaves
- Docker Compose deployment

See `docs/proposal.md` for the full scope.

## Tech

- C# / ASP.NET Core (.NET 10)
- PostgreSQL with EF Core

## Getting started

```
dotnet build
dotnet run --project src/Wane.Api
```

## Structure

```
wane/
├── src/
│   ├── Wane.Domain/          # entities and business rules
│   ├── Wane.Application/     # use cases and interfaces
│   ├── Wane.Infrastructure/  # EF Core, storage, cache
│   └── Wane.Api/             # HTTP API and startup
├── tests/
├── docs/
│   ├── proposal.md           # project scope
│   ├── architecture.md       # the architecture explained
│   └── decisions/            # one short file per decision
└── deploy/                   # Docker / Docker Compose
```

## Contributing

Nobody pushes directly to `main`: create a branch and open a pull request. Every pull request is reviewed automatically by Claude.
