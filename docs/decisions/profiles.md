# Profiles

## Decision

Wane is self-hosted: one deployment is one instance with a single login (see `login.md`). Anyone who logs in can see all profiles, create profiles and manage the library.

A profile is a listener with a name and its own playlists, favourites and listening history. All profiles share the instance library. Profiles have no password. There is no public registration and no account types. "Artist" is metadata on tracks and albums, not an account.

## Why

Wane is deployed by individuals for their own music, so public accounts, artist uploaders and account types don't fit.

## To Decide

- Who can add music: only the instance login, or also profiles.
- Optional PIN per profile.
- Default storage for a home setup (local filesystem, with S3/MinIO optional).
- Update the proposal wording about user accounts and artists uploading.
