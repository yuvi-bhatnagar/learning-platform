# MVC Rules

Use ASP.NET Core MVC with Razor Views.

## Controllers

- Keep controllers thin.
- Validate input.
- Call the appropriate service.
- Return the correct response/view.
- Do not contain complex business rules.

## Razor Views

- Use strongly typed ViewModels.
- Keep logic minimal.
- Do not perform database access from views.
- Reuse partial views where appropriate.
- Keep JavaScript separated into static files where practical.

## Areas

Use:

- Areas/Admin
- Areas/Teacher
- Areas/Student

Keep role-specific controllers and views inside their appropriate areas.

## AJAX

Use AJAX only when it improves the user experience.

Prefer existing endpoints/components rather than creating duplicate endpoints for the same data.