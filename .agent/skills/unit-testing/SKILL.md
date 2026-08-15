---
name: unit-testing
description: >-
  Use this skill when writing unit tests using xUnit, mocking dependencies, and validating
  business logic, status transitions, and schedules.
---

# Unit Testing

This skill governs the creation of unit tests using xUnit to verify domain and service-layer behaviors.

## Guidelines

### Framework & Isolation
- **xUnit**: Use xUnit as the testing framework.
- **Service Focus**: Write unit tests primarily for business logic in the service layer.
- **Mocking**: Mock external dependencies (e.g., DbContext, UserManager, caching, external APIs) using Mock/Fake frameworks so tests run entirely in-memory, are isolated, and are deterministic.
- **No Framework Tests**: Do not write tests to verify default framework behavior (e.g., that ASP.NET Core MVC routing calls an action or that EF Core saves a record, unless validating custom extensions).

### Required Business Rule Tests
Every business feature must include unit tests for the following scenarios:
1. **Happy Path**: Successful execution under valid conditions.
2. **Invalid Input**: Verification of error handling with empty, null, or out-of-range parameters.
3. **Business-Rule Failures**: 
   - Overlapping bookings for both teachers and students.
   - Duplicate bookings of the same availability slot.
   - Active user checks (booking with deactivated students/teachers).
4. **Lecture Status Transitions**:
   - Valid transitions: `Pending` to `Completed`, `Pending` to `Missed`, and `Pending` to `Cancelled`.
   - Invalid transitions: Attempting to transition from `Completed` to any other status, or modifying cancelled/missed lectures.
5. **Feedback Rules**:
   - Restricting feedback to the assigned student.
   - Allowing feedback only for `Completed` lectures.
   - Preventing multiple feedback submissions for the same lecture.
6. **Report Rules**:
   - Restricting report submission to the assigned teacher.
   - Allowing reports only for `Completed` lectures.
   - Preventing modification of reports after final submission.

### Quality & Readability
- **Structure**: Organize tests using the AAA (Arrange, Act, Assert) pattern.
- **Naming Conventions**: Use clear test names indicating the scenario and expected outcome:
  ```csharp
  [Fact]
  public async Task BookLecture_WhenTeacherHasOverlap_ThrowsInvalidBookingException()
  ```
- **Deterministic**: Tests must not rely on static state, database state, current system time (inject an `ISystemClock` or time provider if testing time-dependent actions), or network connectivity.

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
