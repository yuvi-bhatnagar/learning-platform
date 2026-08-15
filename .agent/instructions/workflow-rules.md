# Agent Workflow

Follow this workflow for every requested feature.

## 1. Inspect

Read the relevant existing files before changing anything.

## 2. Understand

Identify:

- Existing implementation
- Dependencies
- Business rules
- Related tests
- Database impact
- Authorization requirements

## 3. Plan Internally

Choose the smallest implementation that fits the existing architecture.

Do not create files/classes unless necessary.

## 4. Implement

Implement the feature end-to-end.

Prefer:

Entity/Database
→ Service
→ ViewModel
→ Controller
→ Razor View
→ Validation
→ Authorization
→ Tests

## 5. Conflict Check

After implementation explicitly check:

- Existing functionality conflicts
- Duplicate functionality
- Duplicate code
- Duplicate queries
- Route conflicts
- Authorization conflicts
- Validation conflicts
- Database constraint conflicts

## 6. Optimization Check

Check:

- Database query count
- Query efficiency
- N+1 issues
- Unnecessary Includes
- Unnecessary data loading
- Pagination
- Index requirements
- Duplicate AJAX calls
- Unnecessary Redis usage

Only optimize when there is a real issue or obvious avoidable inefficiency.

## 7. Test

Run:

- Build
- Relevant unit tests
- Relevant integration tests
- Existing regression tests

## 8. Final Review

Before declaring the task complete:

- Confirm the requested functionality works.
- Confirm tests pass.
- Confirm no unrelated files changed.
- Confirm no duplicate functionality was introduced.
- Confirm no obvious performance/security issue was introduced.

If something cannot be verified, state it clearly.