---
name: redis
description: >-
  Use this skill when integrating, configuring, or using Redis caching and temporary state
  storage in the application.
---

# Redis Caching

This skill governs the integration, configuration, and caching strategies using Redis in the application.

## Guidelines

### Infrastructure & Configuration
- **Local Container**: Redis is set up as local infrastructure and runs as a Docker container managed by Docker Compose.
- **Configuration**: Always load the Redis connection string from `appsettings.json` or environment variables. Do not hard-code connection parameters.
- **Abstraction**: Encapsulate Redis interactions behind a service interface (e.g., `ICacheService`) rather than injecting StackExchange.Redis directly into business logic.

### When to Cache
- **Real Value Only**: Do not use Redis just because it is configured. Only apply caching when there is a clear, justified need (e.g., high-read, low-write data like active teachers list or active availabilities).
- **Graceful Degradation**: The application must function properly even if Redis becomes unavailable (e.g., wrap Redis operations in try-catch blocks and fall back to database queries).

### Expiration & Invalidation
- **TTL (Time to Live)**: Every cached item must have an explicit sliding or absolute expiration time defined (e.g., 5 minutes, 1 hour) to prevent stale or orphaned data.
- **Cache Invalidation**: Write explicit invalidation logic. When cached data changes in the database (e.g., a teacher updates their profile or availability), immediately delete or update the corresponding cache key.
- **Stampede Prevention**: Use appropriate safeguards to avoid cache stampede or loading large datasets repeatedly if multiple requests miss the cache concurrently.

---

## Mandatory Review After Every Implementation

After completing any change, you must verify the following checklist:

1. **Conflict**:
   - Did the new implementation conflict with existing functionality?
   - Did it change existing behavior unintentionally?
   - Are there route, authorization, validation, database, or dependency conflicts?

2. **Duplication**:
   - Did it duplicate an existing method, query, component, ViewModel, service, validation rule, or UI?
   - Can existing functionality be reused?

3. **Architecture**:
   - Is the code in the correct layer?
   - Did business logic accidentally enter the controller or Razor View?
   - Was an unnecessary abstraction introduced?

4. **Performance**:
   - Did it add unnecessary database queries?
   - Is there an N+1 query?
   - Is unnecessary data being loaded?
   - Is pagination needed?
   - Is `AsNoTracking` appropriate?
   - Is an index needed?
   - Are there duplicate AJAX requests?
   - Is Redis actually justified?

5. **Security**:
   - Can another user access this resource?
   - Is server-side authorization present?
   - Is user/resource ownership validated?
   - Is input validated?
   - Are antiforgery protections needed?
   - Are secrets/configuration handled safely?

6. **Testing**:
   - What new business rules were introduced?
   - Are the important success, failure, authorization, and edge cases tested?
   - Do existing tests still pass?

7. **Cleanup**:
   - Remove dead code, debugging code, and unused imports.
   - Remove unnecessary or temporary files/implementations.
