# Project Proposal: Music Streaming Platform Backend

**Date:** November 2026

---

## 1. Summary

This project proposes the design and implementation of the backend of a music streaming platform. The focus is on the server-side challenges of delivering audio at scale: ingesting and transcoding audio files, storing and serving them efficiently, supporting adaptive streaming, and measuring how the system behaves under load.

A player and frontend are outside the scope of this proposal. The backend will be exposed through a documented REST API and tested with API clients and a minimal test page.

## 2. Motivation

Audio streaming is a good example of a backend system where several engineering problems meet: media processing, large file storage, network delivery, caching, concurrency and scalability. It is also a practical way to apply C#/.NET, relational databases, background processing and containerised deployment in a realistic backend system.

## 3. Objectives

**General objective:** build a working, documented and benchmarked backend for a music streaming service.

**Specific objectives:**

1. Design a relational data model for the music catalog, users and playlists.
2. Implement user authentication and authorisation.
3. Implement an upload pipeline that transcodes audio into multiple bitrates asynchronously.
4. Implement audio delivery with HTTP range requests (seeking support) and adaptive streaming (HLS).
5. Introduce caching to reduce latency and database/storage load.
6. Evaluate performance through load testing and report the results.
7. *(Optional)* Implement one performance-critical component in C++ and compare it against an equivalent C# implementation.

## 4. Scope

**In scope**

- Catalog management: artists, albums, tracks, playlists and search
- User accounts, personal library and listening history
- Audio upload, background transcoding and storage
- Streaming endpoints (range requests and HLS)
- Caching layer and performance benchmarks
- API documentation (OpenAPI/Swagger)
- Containerised setup (Docker Compose)
- *Optional:* one self-contained C++ module, benchmarked against a C# baseline (see Section 5)

**Out of scope**

- Recommendation systems
- A full-featured frontend or mobile app
- Payments, subscriptions and royalty management
- DRM

## 5. Proposed Architecture and Technologies

| Component | Technology |
|---|---|
| API | ASP.NET Core Web API (C#) |
| Database | PostgreSQL |
| Cache | Redis |
| Audio storage | S3-compatible object storage (MinIO) |
| Transcoding | FFmpeg, run by a background worker |
| Background jobs | Hangfire or a message queue |
| Streaming | HTTP range requests and HLS |
| Deployment | Docker / Docker Compose |
| Load testing | k6 or equivalent |

**High-level flow:** a track is uploaded through the API, stored in object storage and queued for transcoding. A worker produces several bitrate versions and HLS segments, then updates the catalog. Clients request the stream through the API, which serves it with caching in front of storage.

### Optional: C++ module for performance-critical components

The main system will be written in C#. To explore where native code brings measurable benefits, one self-contained, CPU-bound component may additionally be implemented in C++ and integrated into the backend. Only one candidate will be chosen:

- **Audio analysis:** waveform peak generation and loudness analysis (EBU R128 / ReplayGain).
- **Search index:** an in-memory index (trie or inverted index) for track and artist search and autocomplete, compared against PostgreSQL full-text search.
- **Audio fingerprinting:** detection of duplicate tracks.

**Integration:** the module would be built as a shared library with a C interface and called from C# through P/Invoke, or run as a separate process called by the backend. The streaming layer itself remains in ASP.NET Core, since audio delivery is I/O-bound and would gain little from C++.

**Method:** a C# baseline is implemented first, then the C++ version, and both are compared with the same benchmarks (processing time, memory usage and throughput). The core pipeline does not depend on this module. If it brings no clear benefit, or time is limited, it is dropped, and the benchmark results are still reported as findings.

## 6. Methodology and Phases

The project will be developed incrementally, in layers, so that each phase produces a working system:

1. **Foundation:** requirements, data model, project setup, authentication and catalog API.
2. **Upload and direct streaming:** file upload, storage and streaming with range requests.
3. **Transcoding and adaptive streaming:** background worker, multiple bitrates and HLS.
4. **Optimisation:** caching, performance tuning and load testing.
5. **Optional C++ module:** C# baseline, C++ implementation, integration and comparison benchmarks.
6. **Documentation:** API documentation and results analysis.

## 7. Evaluation

The system will be evaluated on:

- **Functionality:** the full flow from upload to playback works correctly.
- **Performance:** time to first byte, throughput and behaviour with concurrent listeners, with and without caching.
- **Scalability:** how the system behaves as load increases and what its bottlenecks are.
- **Native performance (optional):** processing time and memory usage of the C++ module compared with the C# baseline.
- **Code quality:** automated tests and documentation.

## 8. Content and Licensing

Only royalty-free, Creative Commons or self-produced audio will be used for development and testing (for example, Free Music Archive or Jamendo), so the project does not involve copyrighted material.

## 9. Risks and Mitigation

| Risk | Mitigation |
|---|---|
| Transcoding and HLS are more complex than expected | Deliver direct streaming with range requests first; add HLS afterwards |
| Limited time | Layered development, so each phase is a complete, usable system |
| Limited hardware for load tests | Use containerised environments and scale tests to the available resources |
| C++ module adds build complexity, memory-safety risks and deployment work | Keep it optional and isolated behind a C interface or separate process; the core system works without it |

## 10. Expected Deliverables

- Source code repository
- Running backend with Docker Compose setup
- API documentation
- Performance test results and analysis
- *(Optional)* C++ module with a C# vs C++ benchmark comparison
