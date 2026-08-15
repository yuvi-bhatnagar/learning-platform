---
name: lecture-booking
description: >-
  Use this skill when implementing, modifying, or testing the core lecture booking flow,
  schedule conflict resolution, status transitions, and feedback/report rules.
---

# Lecture Booking Domain

This skill governs the business-critical domain of lecture bookings, status transitions, student feedback, and teacher reports.

## Core Domain Rules

### Lecture Properties
- **Duration**: Each lecture is exactly **30 minutes**.
- **Entities**:
  - `TeacherProfile` / `Teacher` (Must be active to be booked or update availability)
  - `StudentProfile` / `Student` (Must be active to book a lecture)
  - `TeacherAvailability` (Defines slots when the teacher is available for booking)
  - `Lecture` (Represents the 30-minute booking session)

### Lecture Status Transitions
- **Statuses**: `Pending`, `Completed`, `Missed`, `Cancelled`.
- **Valid Transitions**:
  - `Pending` → `Completed`
  - `Pending` → `Missed`
  - `Pending` → `Cancelled`
- **Terminal States**: `Completed`, `Missed`, and `Cancelled` are final terminal states. Once a lecture reaches any of these statuses, it is locked and **cannot be modified** or transitioned to another state.

### Booking Rules
Before confirming any lecture booking, the service layer must validate:
1. **Active Users**: Both the teacher and student profiles must be active.
2. **Teacher Availability**: The selected time slot must correspond to an active availability slot defined by the teacher.
3. **Exact Slot Duration**: The difference between the start time and end time of the booking must be exactly 30 minutes.
4. **No Overlapping Bookings (Teacher)**: The teacher must not have another lecture that overlaps with the requested time slot.
5. **No Overlapping Bookings (Student)**: The student must not have another lecture that overlaps with the requested time slot.
6. **No Double Booking**: An availability slot cannot be booked more than once.
7. **Concurrency Safety**: Use database-level constraints (e.g., unique index on `(TeacherId, StartTime)`), optimistic concurrency tokens, or transactions to guarantee that two students cannot book the same slot at the exact same time.

### Feedback Rules
- **Eligibility**: Only the specific student assigned to the lecture is authorized to submit feedback.
- **Prerequisite**: Feedback can only be submitted for a lecture that has a status of `Completed`.
- **Limit**: Only **one** feedback entry is permitted per lecture.

### Report Rules
- **Eligibility**: Only the specific teacher assigned to the lecture is authorized to submit the lecture report.
- **Prerequisite**: The report can only be submitted for a lecture that has a status of `Completed`.
- **Immutability**: Prevent modifications to the report after it has been finalized and submitted.

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
