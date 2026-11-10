# Architecture

## Introduction

This document describes the architecture of the Music Streaming Platform Backend project. It explains how the system is organised, how its parts relate to each other, and the reasoning behind the main structural decisions.

## Chosen Architecture

The architecture of the project will be **Onion Architecture**.

## What is Onion Architecture?

Onion Architecture is a way of organising an application into concentric layers, like the layers of an onion. It was introduced by Jeffrey Palermo in 2008 and is closely related to Clean Architecture and Hexagonal Architecture (ports and adapters). All of them share the same goal: keep the business logic at the centre of the application, independent of frameworks, databases and other technical details.

### The Dependency Rule

The main rule of the architecture is that **dependencies always point inward**. An inner layer knows nothing about the layers around it. Outer layers can depend on inner layers, but inner layers never depend on outer ones.

```
┌────────────────────────────────────────────────┐
│  Infrastructure / API (outer layer)            │
│  ┌──────────────────────────────────────────┐  │
│  │  Application (services, interfaces)      │  │
│  │  ┌────────────────────────────────────┐  │  │
│  │  │  Domain (entities, business rules) │  │  │
│  │  └────────────────────────────────────┘  │  │
│  └──────────────────────────────────────────┘  │
└────────────────────────────────────────────────┘

          Dependencies point inward  →  →  →
```

When an inner layer needs something from the outside, such as saving data or storing a file, it defines an **interface** and the outer layer provides the implementation. This is the Dependency Inversion Principle (the "D" in SOLID). The implementations are connected to the interfaces at application startup using dependency injection.

### The Layers

**1. Domain (the core)**
The centre of the application. It contains the entities, value objects and business rules that describe the problem the system solves. It has no dependencies on any other layer or external library, so it can be understood and tested on its own.

**2. Application**
Surrounds the domain and contains the application's use cases. It holds the services that coordinate the work, the data transfer objects (DTOs), and the interfaces that describe what the application needs from the outside world (for example, access to data, file storage or caching). It depends only on the Domain layer.

**3. Infrastructure**
The outer layer that deals with technical details. It implements the interfaces defined in the Application layer: database access, file storage, caching, external tools and any other integration. This layer can be changed or replaced without affecting the layers inside it.

**4. Presentation / API**
The entry point of the system. It receives requests from clients, calls the Application layer and returns responses. It also contains the composition root, the place where dependency injection connects the interfaces to their implementations.

### Comparison with Traditional Layered Architecture

In a traditional layered architecture (controller, service and repository), each layer depends on the one below it. The business logic therefore ends up depending on the data access layer and, indirectly, on the database technology.

In Onion Architecture the direction is inverted. The business logic sits in the centre and the data access layer depends on it, by implementing interfaces that the business logic defines. The familiar controller, service and repository concepts still exist, but they are arranged around the domain and not stacked on top of the database.

### Benefits

- **Independence from technology:** the core logic does not depend on a specific database, framework or external service, so these can be replaced with little effort.
- **Testability:** the Domain and Application layers can be tested without a database or other infrastructure, by replacing the interfaces with test doubles.
- **Maintainability:** each layer has a clear responsibility, which keeps the code organised and makes changes easier to isolate.
- **Flexibility:** different implementations of the same interface can be used in different environments, for example a lightweight setup for local development and a more scalable one for performance testing.
