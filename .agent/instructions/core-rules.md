# Core Agent Rules

- Follow the project context and existing architecture before making changes.
- Prefer the smallest correct implementation.
- Do not introduce unnecessary abstractions, libraries, patterns, or files.
- Reuse existing code when appropriate.
- Do not duplicate existing functionality.
- Do not modify unrelated files.
- Do not refactor large portions of the project unless required.
- Keep changes focused on the requested feature.
- Prefer simple, readable code over clever code.

## Before Every Change

Inspect the relevant existing code first.

Check:

1. Does this functionality already exist?
2. Is there an existing service, helper, ViewModel, component, or query that can be reused?
3. Will this implementation conflict with existing behavior?
4. Will it introduce duplicate logic?
5. Will it create unnecessary database queries?
6. Will it introduce a performance problem?
7. Will it break existing tests?
8. Does authorization still work correctly?
9. Does validation still work correctly?
10. Is any existing code now obsolete?

## After Every Change

Verify:

- Build succeeds.
- Relevant tests pass.
- Existing functionality remains intact.
- No duplicate implementation was introduced.
- No unnecessary database calls were introduced.
- No obvious performance regression exists.
- No authorization or security regression exists.
- No dead code was introduced.

Do not stop at compilation if the feature has behavior that can be tested.