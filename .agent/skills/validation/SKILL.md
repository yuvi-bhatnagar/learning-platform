---
name: validation
description: >-
  Use this skill when implementing input validation, DataAnnotations, ModelState checks,
  custom validation rules, client-side validation, and resource ownership checks.
---

# Validation

This skill governs input, business, and security validation rules across the application layers.

## Guidelines

### Input Validation (DataAnnotations)
- Use standard C# `System.ComponentModel.DataAnnotations` on ViewModels to validate basic format, presence, and ranges of input parameters:
  - `[Required]` for non-nullable or required fields.
  - `[StringLength]` and `[MinLength]` to prevent database truncation or overflow issues.
  - `[Range]` for numeric inputs.
  - `[EmailAddress]` or custom regex pattern attributes for formatting.
- Keep validation annotations clean and self-explanatory.

### Controller-Level Validation (ModelState)
- Always check `ModelState.IsValid` in state-changing controller actions (POST, PUT, DELETE).
- If the model state is invalid:
  - Render the target view and pass the current ViewModel to show feedback.
  - Return `BadRequest(ModelState)` or a structured JSON response for AJAX or API calls.

### Client-Side Validation
- Enable unobtrusive client-side validation on forms using the standard partial scripts:
  ```html
  <partial name="_ValidationScriptsPartial" />
  ```
- Use input tag helpers so client-side metadata is automatically derived from C# DataAnnotations.
- **Critical Rule**: Client-side validation is solely for user experience. It must never replace server-side validation. All inputs must be fully validated on the server.

### Business-Level Validation
- Complex checks that depend on external resources, database state, or domain conditions (e.g., check if a student is active, if the teacher has overlapping lectures, or if a slot is already booked) must be handled inside the service layer.
- Throw custom exceptions or return failure results when business validation fails.
- Avoid duplicating complex validation logic. Encapsulate validation helper methods or rules inside specific services to maintain consistency.

### Ownership & Permission Validation
- Always validate that the user requesting the action owns or has rights to the underlying resource on the server.
- Extract the authenticated user's ID securely in the backend, and compare it against the resource's owner ID (e.g., checking if `Lecture.StudentId == loggedInStudentId` before allowing them to post feedback).

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
