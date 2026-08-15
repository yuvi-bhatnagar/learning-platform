---
name: ef-core
description: >-
  Use this skill when defining database entities, DbContext, configuring relationships,
  creating EF Core migrations, and optimizing database queries.
---

# Entity Framework Core Development

This skill governs data access, entity modeling, and database integration using Entity Framework Core with SQL Server.

## Guidelines

### Entities & DbContext Configuration
- **Entity Definition**: Entities must represent database tables. Use appropriate C# types matching SQL Server types.
- **Fluent API Configuration**: Prefer using EF Core's Fluent API in `OnModelCreating` to configure relationships, precision, constraints, and indexes, keeping entity classes clean of database-specific decorations.
- **Database Constraints**: Always enforce integrity constraints at the database level where appropriate:
  - Configure foreign keys for relational integrity.
  - Apply length limits on strings (`HasMaxLength`).
  - Make required columns non-nullable.
  - Set unique constraints or indexes on combination fields (e.g., preventing duplicate lecture slots).

### Migrations
- Use standard EF Core CLI tools for migrations:
  - Add migrations: `dotnet ef migrations add <MigrationName>`
  - Apply migrations: `dotnet ef database update`
- **Never Modify Existing Migrations**: Never edit migrations that have already been run or committed. Create a new migration to modify schema or constraints.
- Always review generated migrations before applying them to ensure they match expectations.

### Queries & Performance
- **Async Data Access**: Use async equivalents for all database I/O (`ToListAsync`, `FirstOrDefaultAsync`, `SaveChangesAsync`, etc.).
- **Avoid N+1 Queries**: Never run database queries inside loops. Avoid loading related entities sequentially.
- **Eager Loading & Projections**:
  - Use `.Include()` / `.ThenInclude()` only when necessary. Avoid over-fetching relationships that won't be used.
  - Use `.Select()` projection to query only the required columns and project directly into ViewModels or DTOs.
- **Read-Only Queries**: Always append `.AsNoTracking()` for read-only queries.
- **Pagination & Filtering**:
  - Implement pagination for lists or tables using `.Skip()` and `.Take()`.
  - Perform filtering on the SQL Server side using `.Where()`, not in-memory after fetching.
- **Indexes**: Configure indexes on foreign keys and columns frequently used in `Where` clauses or sorting.

### Transactions & Concurrency
- Use EF Core transactions or database transactions when multiple save operations must succeed or fail as a single unit.
- Implement concurrency checks (e.g., using optimistic concurrency tokens, row versions, or specific lock constraints) to safely manage booking operations.

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
