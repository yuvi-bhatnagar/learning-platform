# Testing Rules

Testing is part of every feature.

For each business feature:

1. Implement the feature.
2. Identify business rules.
3. Add tests for valid behavior.
4. Add tests for invalid behavior.
5. Add authorization tests where relevant.
6. Add edge-case tests.
7. Run existing tests.
8. Confirm no regression.

## Unit Tests

Focus primarily on business logic in services.

Important examples:

- Valid lecture status transition
- Invalid lecture status transition
- Duplicate booking
- Overlapping booking
- Unauthorized operation
- Invalid feedback
- Invalid report submission

Do not waste tests on trivial framework behavior.

## Integration Tests

Use integration tests where behavior depends on:

- MVC routing
- Authorization
- EF Core
- Database interaction
- Full request/response flow

## Test Requirement

A feature is not complete merely because it builds.

The relevant tests must pass.