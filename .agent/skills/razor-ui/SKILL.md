---
name: razor-ui
description: >-
  Use this skill when writing or editing Razor Views, layouts, partial views, Bootstrap
  layouts, forms, jQuery, and AJAX interactions.
---

# Razor UI Development

This skill governs the presentation layer, form handling, frontend validation, styling, and client-side interactions in the MVC application.

## Guidelines

### Razor Views & Layouts
- **Strongly Typed Views**: Always use strongly typed ViewModels. Declare `@model MyViewModel` at the top of each view. Do not use dynamic `ViewData` or `ViewBag` for key data structures.
- **Keep Logic Minimal**: Avoid complex logic, calculations, or direct database access inside views. Razor Views must only read properties from the ViewModel and display them.
- **Layouts**: Use the main layout (`_Layout.cshtml`) for consistent header, footer, navigation, and sidebar components. Keep page-specific content within `@RenderBody()` and page-specific scripts within `@section Scripts { ... }`.
- **Partial Views**: Extract reusable UI components (e.g., availability calendar cards, booking status badges, feedback tables) into partial views (`_PartialName.cshtml`) to avoid code duplication.

### Forms & Validation
- **Tag Helpers**: Use built-in ASP.NET Core Tag Helpers (`asp-for`, `asp-action`, `asp-controller`, `asp-area`, `asp-validation-for`, `asp-validation-summary`).
- **Validation Messages**: Always include validation elements near form inputs:
  ```html
  <span asp-validation-for="PropertyName" class="text-danger"></span>
  ```
- **Validation Summary**: Use `<div asp-validation-summary="ModelOnly" class="text-danger"></div>` at the top of forms to show cross-property or server-side model errors.

### CSS, Bootstrap, and Scripts
- **Styling**: Use Bootstrap classes for modern, clean, responsive styling. Custom layouts should use CSS Flexbox or Grid. Save custom stylesheet changes in static files inside `wwwroot/css/`.
- **Scripts**: 
  - Keep JavaScript organized and separate from HTML markup. Avoid inline JS handlers (like `onclick="..."`). Use jQuery or vanilla JS events inside static files or within `@section Scripts`.
  - Include validation scripts on forms using:
    ```html
    @section Scripts {
        <partial name="_ValidationScriptsPartial" />
    }
    ```

### AJAX & Client-Side Actions
- **User Experience**: Use AJAX requests (via jQuery `$.ajax` or `$.post`) only when it significantly improves the user experience (e.g., dynamically loading teacher available slots without full page reloads, liking feedback, or checking real-time conflicts).
- **Prevent Multi-Submit**: Always disable buttons or show a loading indicator during AJAX or form submission to prevent duplicate submissions or duplicate requests.
- **Error Handling**: Properly handle AJAX failures on the client side, displaying clear error messages instead of failing silently.

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
