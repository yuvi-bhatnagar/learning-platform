---
name: identity-authorization
description: >-
  Use this skill when configuring or using ASP.NET Core Identity, roles, authentication,
  authorization, or checking resource ownership.
---

# Identity and Authorization

This skill governs user authentication, authorization, role management, and resource ownership verification.

## Guidelines

### Core Setup & Roles
- **ApplicationUser**: Use `ApplicationUser` extending `IdentityUser` as the central identity class.
- **Roles**: The application supports four roles:
  - `SuperAdmin` (highest administrative access, manages Admins/SuperAdmins/Teachers/Students)
  - `Admin` (manages Teachers/Students, views reports/feedback)
  - `Teacher` (manages availability, view lectures, submit reports)
  - `Student` (views teachers, books lectures, submits feedback)

### Authentication Flow
- **Registration**: Student registration is public. All other roles (Admin, SuperAdmin, Teacher) are created and managed by administrative users in the `/admin` area.
- **Log In / Log Out**: Handled using Identity's `SignInManager` and standard cookie authentication.
- **Password Handling**: Never write custom password hashing or storage logic. Always delegate password verification, hashing, and complexity rules to ASP.NET Core Identity.

### Authorization Checks
- **Server-Side Authorization**: Always validate roles and policies on the server. Never rely on client-side routing, hidden UI elements, or hidden fields to enforce security.
- **Role Attributes**: Annotate controllers or action methods with role checks:
  - `[Authorize(Roles = "SuperAdmin")]`
  - `[Authorize(Roles = "Admin")]`
  - `[Authorize(Roles = "Teacher")]`
  - `[Authorize(Roles = "Student")]`
- **Never Trust Client Inputs**: Never trust client-supplied user IDs or role claims. Always retrieve the current user's ID securely using the server context:
  ```csharp
  var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
  ```

### Resource Ownership & Policies
- **Verify Resource Ownership**: Before modifying or viewing sensitive user-specific data, confirm that the authenticated user owns the resource.
  - A Student can only view or give feedback on their own lectures.
  - A Teacher can only update availability or submit reports for their own lectures.
  - Verify this ownership in the backend (services or controllers) before executing actions.
- **Access Denied**: Return appropriate HTTP response statuses for unauthorized requests:
  - For standard MVC view actions: redirect to `/Account/AccessDenied`.
  - For API/AJAX calls: return `401 Unauthorized` or `403 Forbidden`.

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
