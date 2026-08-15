---
name: mvc-development
description: >-
  Use this skill when developing or modifying ASP.NET Core MVC controllers, routing,
  areas, actions, model binding, ViewModels, and HTTP responses.
---

# MVC Development

This skill governs the development of ASP.NET Core MVC components.

## Guidelines

### Controllers
- **Thin Controllers**: Controllers must only handle HTTP-level concerns (routing, parsing inputs, validating `ModelState`, invoking service layer methods, and mapping results to HTTP responses).
- **No Business Logic**: Keep business logic completely out of controllers. All logic belongs in the service layer.
- **Dependency Injection**: Use constructor injection to inject service interfaces or classes. Do not instantiate services manually or resolve them from `IServiceProvider` at runtime.

### Routing & Areas
- Organize role-specific code into MVC Areas:
  - `Areas/Admin/` for SuperAdmin and Admin actions.
  - `Areas/Teacher/` for Teacher actions.
  - `Areas/Student/` for Student actions.
- Decorate area controllers with `[Area("AreaName")]` and place them in the correct folder structure: `Areas/[AreaName]/Controllers/[Name]Controller.cs`.
- Use attribute routing (`[Route("...")]`) or consistent conventional routing to keep URLs clear and predictable.

### Actions & HTTP Methods
- Explicitly annotate action methods with HTTP verbs (e.g., `[HttpGet]`, `[HttpPost]`).
- Use `[ValidateAntiForgeryToken]` on all state-modifying `[HttpPost]` actions.
- Return appropriate HTTP responses:
  - Return `View(viewModel)` for HTML rendering.
  - Return `RedirectToAction` or `RedirectToRoute` after successful POST requests (POST-Redirect-GET pattern).
  - Return `Ok(data)`, `BadRequest(message)`, `NotFound()`, `Forbid()`, or `Challenge()` for AJAX/API requests.

### Model Binding & ViewModels
- Never pass database entities directly to Razor Views or accept them as input parameters to POST actions.
- Always use strongly typed ViewModels prefixed or suffixed correctly (e.g., `TeacherListViewModel`, `BookLectureInputModel`).
- Check `ModelState.IsValid` in POST actions. If invalid, re-render the view with the input ViewModel to display error messages.

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
