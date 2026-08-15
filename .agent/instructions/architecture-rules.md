# Architecture Rules

Use a simple maintainable ASP.NET Core MVC monolith.

Architecture:

Controller
    ↓
Service
    ↓
EF Core / Repository when actually required
    ↓
SQL Server

Views should use ViewModels rather than exposing database entities unnecessarily.

## Rules

- Controllers handle HTTP concerns only.
- Business logic belongs in services.
- Entities represent domain/database data.
- ViewModels represent UI input/output.
- Do not put business rules inside Razor Views.
- Do not put significant business logic inside controllers.
- Do not create repositories automatically for every entity.
- Use EF Core directly from services when a repository adds no value.
- Introduce an abstraction only when it provides a real benefit.
- Keep dependencies flowing in one direction.
- Avoid circular dependencies.
- Avoid static state for application data.