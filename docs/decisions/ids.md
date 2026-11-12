# IDs

## Decision

Entities use time-ordered GUIDs (version 7) as primary keys. They are generated in the entity constructor with `Guid.CreateVersion7()` and stored as `uuid` in PostgreSQL. The database does not generate ids, and callers do not set them.

## Why

The Domain layer does not depend on the database, so an entity gets its identity when it is created, with no database round trip. The id is also known before the row is saved, which helps the upload flow (storing the audio file under the track id first). Compared with random GUIDs, version 7 values increase over time, so PostgreSQL indexes behave almost like integer keys, with few page splits. Ids in URLs are not guessable.

Generating the id in the constructor means an entity never exists with an empty id, which fits the rule that entities refuse invalid state themselves (see `validation-and-mapping.md`).
