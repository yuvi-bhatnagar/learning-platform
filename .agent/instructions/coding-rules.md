# Coding Rules

- Follow standard C# conventions.
- Use nullable reference types.
- Prefer async/await for database and I/O operations.
- Pass CancellationToken where appropriate.
- Use dependency injection.
- Avoid magic strings and magic numbers.
- Use enums/constants where appropriate.
- Keep methods small and focused.
- Avoid deeply nested conditionals.
- Prefer guard clauses.
- Use meaningful names.
- Do not catch exceptions unless they can be handled meaningfully.
- Do not silently swallow exceptions.
- Use ILogger for application logging.
- Do not use Console.WriteLine for application logging.
- Avoid premature optimization.
- Avoid premature abstraction.

## DRY

Before adding code, search for existing equivalent logic.

Do not create:

- Duplicate validation
- Duplicate database queries
- Duplicate authorization logic
- Duplicate status-transition logic
- Duplicate mapping logic

Extract shared logic only when duplication is real and meaningful.