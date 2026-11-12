# Development guide

How to run Wane locally. For why things are set up this way, see `docs/decisions/`.

## Requirements

- .NET 10 SDK
- Docker (for PostgreSQL)
- The `dotnet-ef` tool, pinned to the EF Core version used by the project:

```
dotnet tool install --global dotnet-ef --version 10.0.14
```

## Run the database

PostgreSQL runs in Docker Compose. Copy the example environment file and fill in the values:

```
cp deploy/.env.example deploy/.env
docker compose -f deploy/docker-compose.yml up -d
```

`deploy/.env` is not committed. The values in it are used by Docker Compose only.

## Configure the application

The application reads its configuration from `appsettings.json`, `appsettings.Development.json`, user secrets and environment variables (see `docs/decisions/configuration.md`). The database password is never stored in a JSON file. For local development, store it in user secrets on the Host project, using the same password as in `deploy/.env`:

```
dotnet user-secrets set "Database:Password" "<password>" --project src/Wane.Host
```

## Run the application

```
dotnet build
dotnet run --project src/Wane.Host
```

## Database migrations

The schema is managed with EF Core migrations. The migrations live in `Wane.Infrastructure`, and `Wane.Host` is the startup project, because it provides the configuration.

`dotnet ef` does not read the launch profiles and would run as Production, so the commands set the environment to Development.

Create a migration after changing an entity or its configuration (replace `<Name>`):

```
env ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations add <Name> \
  --project src/Wane.Infrastructure \
  --startup-project src/Wane.Host \
  --output-dir Persistence/Migrations
```

Apply the pending migrations to the database:

```
env ASPNETCORE_ENVIRONMENT=Development dotnet ef database update \
  --project src/Wane.Infrastructure \
  --startup-project src/Wane.Host
```

Always read the generated migration before applying it. To undo the last migration, if it has not been applied yet:

```
dotnet ef migrations remove \
  --project src/Wane.Infrastructure \
  --startup-project src/Wane.Host
```
