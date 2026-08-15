# Database Rules

Use EF Core with SQL Server.

- Database schema must represent actual business rules.
- Use foreign keys.
- Use appropriate indexes.
- Use unique constraints where required.
- Use transactions when multiple operations must succeed together.
- Do not load unnecessary columns.
- Use projections for list pages when appropriate.
- Use AsNoTracking for read-only queries when appropriate.
- Avoid N+1 queries.
- Avoid unnecessary Include calls.
- Do not retrieve entire tables when pagination/filtering is possible.
- Use pagination for large listings.
- Validate important business constraints on the server.
- Do not rely only on frontend validation.

## Booking

The database and application must protect against:

- Duplicate bookings
- Teacher schedule conflicts
- Student schedule conflicts
- Overlapping lectures

Concurrency must be considered when booking slots.

## Migrations

- Create migrations for schema changes.
- Never manually modify an existing migration that may already have been applied.
- Review generated migrations before applying them.