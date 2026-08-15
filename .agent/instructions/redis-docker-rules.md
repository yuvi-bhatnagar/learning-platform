# Redis and Docker Rules

Use Docker for local infrastructure.

Redis should run locally through Docker.

Use Docker Compose for local infrastructure.

Do not add Redis to the application just because it is available.

Use Redis only when there is a clear requirement such as:

- Caching
- Temporary data
- Other explicitly required shared state

Keep Redis access behind an abstraction/service.

Redis must be configurable through application configuration.

Do not hard-code Redis connection details.

The application must remain understandable and testable without requiring complicated Redis-specific logic.