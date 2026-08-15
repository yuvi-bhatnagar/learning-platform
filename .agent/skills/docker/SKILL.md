---
name: docker
description: >-
  Use this skill when managing local development infrastructure using Docker, Docker Compose,
  SQL Server, and Redis containers.
---

# Docker and Infrastructure

This skill governs the local development container environment using Docker and Docker Compose.

## Guidelines

### Local Infrastructure
- **Docker Compose**: Use `docker-compose.yml` to define and manage external dependency containers (SQL Server and Redis).
- **Local Application Run**: During active development, the ASP.NET Core MVC application runs directly on the host machine (via Visual Studio, VS Code, or `dotnet run`) while connecting to the containerized databases and cache.
- **Simplicity**: Keep Docker configuration simple. Do not add complex orchestration configurations or containerize unnecessary auxiliary tools.

### Configuration & Environment Variables
- **Secrets Management**: Never hard-code passwords, tokens, or connection strings in code or Dockerfiles.
- **External Configuration**: Pass connection credentials, Redis endpoints, and ports via environment variables or the `docker-compose.yml` environment section.
- **Connection Strings**: Ensure standard database connections retrieve their sources from configuration providers (e.g., `Configuration.GetConnectionString("DefaultConnection")`).

### Setup Commands
- Developers should be able to spin up the local development infrastructure using a single command:
  ```bash
  docker compose up -d
  ```
- Ensure the containers have persistent data volumes mapped locally so database states are not lost when containers are restarted.

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
