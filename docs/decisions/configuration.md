# Configuration

## Decision

Configuration is loaded once, by the host (`Wane.Host`), with the default ASP.NET Core providers and their default order: `appsettings.json`, `appsettings.{Environment}.json`, user secrets (Development only), environment variables, command-line arguments. No providers are added or reordered by hand.

Settings are read with the options pattern. Each concern has its own section (for example `Database`) bound to a typed options class. Each layer registers its own options in its own registration method (for example `AddInfrastructure`), which the Host calls and passes `IConfiguration` to. Options that only Infrastructure needs live in Infrastructure. Options that Application logic depends on live in Application. Domain has no configuration. Options are validated at startup, so a missing value stops the app with a clear message.

Where values live:
- `appsettings.json` and `appsettings.Development.json` are committed and hold only non-secret settings. Values that must be supplied per environment (database host, name, username) are empty in `appsettings.json`.
- Secrets (the database password, later the token signing key) are never in JSON files. In development they are stored with user secrets on the Host project. In deployment they are environment variables.
- Docker Compose reads `deploy/.env` and passes the values to the containers as environment variables (`Database__Password`, `Database__Host`, and so on). The `.env` file is not committed. `deploy/.env.example` is committed and lists every variable with placeholder values.

## Why

The default providers already give the right precedence for this setup: environment variables override the JSON files, so Docker can configure the app without changing any file. The options pattern replaces string keys scattered around the code with typed settings, keeps Application free of `IConfiguration`, and lets startup validation catch missing values early. The same `deploy/.env` drives Postgres and the API, so the password is written in one place. Each self-hosted instance keeps its own `.env`, which fits how Wane is deployed.

## Possible change: Docker secrets

Environment variables are visible with `docker inspect`. Secrets could instead be mounted as files and read with a `_FILE` convention (the Postgres image supports `POSTGRES_PASSWORD_FILE`; the API would need a small step or the key-per-file provider). `login.md` already commits to the `_FILE` form for the login password.
- Pros: the value does not appear as an environment variable.
- Cons: extra code in the API and a slightly more complex Compose file.

## To Decide

- Whether to add `_FILE` / Docker secrets support, and for which secrets.
- Where the token signing key comes from (see `login.md`).
- Whether the database settings stay as separate parts (host, port, name, username, password) or become one connection string.
- Where shared options such as the token settings live, since both Api and the code that issues tokens need them.
- Whether `POSTGRES_DB` moves into `.env`, so the database name is defined in one place.
