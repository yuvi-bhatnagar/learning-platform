---
name: service-layer
description: >-
  Use this skill when implementing services, business logic, dependency injection
  registration, database transaction boundaries, validation, and error handling.
---

# Service Layer Development

This skill governs the design, implementation, and boundaries of the application's business logic layer.

## Guidelines

### Service Encapsulation & Interfaces
- **Business Logic belongs in Services**: All validation of business rules, updates to database state, calculations, and orchestrations must happen in the service layer. Keep controllers and views clean.
- **Interfaces**: Create interfaces (e.g., `ILectureService`, `ITeacherService`) only when they add real value, such as allowing mocking in unit tests or facilitating different implementations. If an interface adds no value, write the service class directly.
- **Dependency Injection Lifetime**: Register services in `Program.cs` with the appropriate lifetime (typically `Scoped` for services accessing the database via EF Core DbContext).

### Business Rules & Server Validation
- **Do Not Trust Clients**: Services must never assume that input from the controller or frontend is correct or fully authorized.
- **Enforce Domain Constraints**: The service layer is the final line of defense. It must validate all business constraints (e.g., ensuring a teacher is active before booking, verifying schedule overlaps, checking valid transitions).
- **Resource Ownership**: Services must verify that the user performing the action is allowed to perform it on that resource.

### Transactions & Consistency
- **Unit of Work**: Database state changes within a service method should be saved atomically. Use `SaveChangesAsync()` to commit changes.
- **Explicit Transactions**: If a service method performs multiple sequential database writes that depend on each other, wrap them in an explicit transaction (`using var transaction = await _context.Database.BeginTransactionAsync();`) to ensure data remains consistent if an error occurs.

### Mapping & DTOs
- Perform the mapping between domain entities and view models/DTOs in the service layer or controller.
- Prevent exposing domain entities to the outer layer unless they are simple data holders with no behavioral side effects.

### Error Handling
- Do not let low-level database or framework exceptions bubble up raw to the client.
- Handle expected business rule failures by returning a structured result object (e.g., `Result.Success()` or `Result.Failure(errorMessage)`) or by throwing custom, well-defined domain exceptions (e.g., `InvalidBookingException`).
- Catch and log exceptions appropriately, returning user-friendly messages for any unhandled failures.

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
