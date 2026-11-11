# Login

## Decision

An instance has one login: a username and password. On the first start, if no login exists, Wane creates it from the env variables `WANE_USERNAME` and `WANE_PASSWORD` (the `_FILE` form is also supported, for Docker secrets). If no password is given, Wane generates one and prints it once in the logs.

The password is stored hashed in the database. After the first start the database is the source of truth, the env variables are no longer used, and the password can be changed from the app.

After login, the API issues short-lived tokens validated by the ASP.NET Core JWT bearer middleware. Hashing uses the framework's `PasswordHasher`, with no custom cryptography. The login endpoint has rate limiting or lockout. Wane does not handle HTTPS itself and must run behind a reverse proxy with HTTPS.

## Why

Env variables only would leave a plaintext password in configuration and make it impossible to change from the app. A first-run setup screen would let whoever reaches the server first claim it. Using env variables or a generated password only on the first start avoids both.

## To Decide

- Token lifetime and whether refresh tokens are needed.
- Where the token signing key comes from (env variable, or generated on first start and stored).
- How streaming players that cannot send an `Authorization` header authenticate.
- Rate limit or lockout settings.
