# Validation and mapping

## Decision

**Validation** has two layers:
- Domain invariants: entities refuse invalid state themselves (for example an empty playlist name), wherever the data comes from. `User` and `Profile` do this with a constructor that generates the id and checks the rules, private setters, and one method per change (for example changing the password hash or renaming a profile). Each method checks its rule, sets the value and updates `UpdatedAt`. The other entities keep plain setters until they have rules worth protecting. Services call these methods and never set the properties directly.
- Request validation: data annotations (`[Required]`, `[StringLength]` and similar) on the DTOs. With `[ApiController]` on the controllers, invalid input becomes a 400 response with field errors automatically.

Rules that depend on stored data (for example a duplicate profile name) are business checks in the services and throw the matching exception, as described in `error-handling.md`.

**Mapping** between entities and DTOs is written by hand, as extension methods next to the DTOs in Application. For reads, repositories can project straight to DTOs in the query, which removes much of the mapping.

## Why

It is the lowest-ceremony option that works with controllers out of the box, with no extra dependencies. The project is small, and explicit mapping is checked by the compiler, fast and easy to follow.

Methods on the entity put each rule in one place, so no service can store an invalid username or name by forgetting a check, and the rules are tested as plain domain unit tests with no database. `User` and `Profile` are the entities with real rules. For entities that only hold descriptive data, a method that just assigns a value would be ceremony.

## Possible change: FluentValidation

If data annotations become too limited, validators could be written with FluentValidation, one per DTO in Application.
- Pros: expressive rules, testable on their own, kept separate from the DTOs.
- Cons: an extra dependency, more files, and validators must be invoked explicitly.

## Possible change: a mapping library

If hand-written mapping becomes too repetitive, a library could be used.
- Mapperly: generates the mapping code at compile time, with no runtime reflection. It is the preferred candidate.
- Mapster: free and fast, and can also generate code at build time.
- AutoMapper: reflection-based with a lot of hidden behaviour, and recent versions need a commercial license.

## To Decide

- When hand-written mapping is repetitive enough to adopt a library.
- When data annotations stop being enough and FluentValidation is added.
