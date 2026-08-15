---
name: integration-testing
description: >-
  Use this skill when writing integration tests for controllers, routes, authentication,
  authorization, and database access.
---

# Integration Testing

This skill governs the creation of integration tests to verify MVC pipelines, authentication/authorization, and actual database queries.

## Guidelines

### Purpose & Scope
- **When to Use**: Use integration tests when the behavior depends on the real ASP.NET Core request pipeline:
  - MVC routing matches correct action methods.
  - Authentication and custom authorization policies/roles are applied correctly (e.g., verifying that a student gets `403 Forbidden` when hitting `/admin`).
  - Database queries retrieve and store data correctly (verifying EF Core mappings and actual SQL Server constraints/triggers).
  - End-to-end user request-to-response flows.
- **Unit vs. Integration**: Always prefer unit tests for isolated business logic. Use integration tests to verify wiring, integration, and security layers.

### Setup & Infrastructure
- **WebApplicationFactory**: Use `Microsoft.AspNetCore.Mvc.Testing` and `WebApplicationFactory<TEntryPoint>` to spin up an in-memory test server for requests.
- **Test Database**: Use an isolated test database (e.g., SQL Server LocalDB, containerized SQL Server, or EF Core In-Memory database if database-specific structures are not required). Ensure the schema is migrated before running tests.
- **Clean State**: Reset the database state between tests to ensure runs are isolated and repeatable.

### Verification Areas
1. **Routing and Security**: Verify that requests to protected routes return redirect-to-login (`302`) or access denied (`403`) responses for unauthenticated or unauthorized users.
2. **ModelState and Binding**: Verify that malformed JSON or invalid form-POST payloads are rejected with validation errors.
3. **Database Interactions**: Verify that unique database constraints (e.g., booking unique indexes) successfully block duplicate insertions and throw appropriate db update exceptions.

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
