---
name: azure-devops
description: >-
  Use this skill when managing Azure DevOps pull requests, commits, pipelines, and setting up
  CI/CD builds.
---

# Azure DevOps and Development Workflow

This skill governs repository practices, Git workflows, Azure DevOps integrations, and automated pipelines.

## Guidelines

### Git Commits & Branches
- **Small, Logical Commits**: Commit changes in small, self-contained units that focus on a single aspect (e.g., "Add teacher availability db model" or "Implement booking overlap validation").
- **Clear Commit Messages**: Use prefix-based, descriptive messages:
  - `feat: ...` for new features
  - `fix: ...` for bugs
  - `test: ...` for adding tests
  - `refactor: ...` for structural changes
- **Branch Naming**: Match branches to user stories or tasks from Azure Boards (e.g., `user/feature/123-lecture-booking`).

### Pull Requests (PRs) & Code Review
- **Single Focus**: Ensure each PR addresses a specific, limited scope to make code review efficient.
- **Pre-PR Checklist**: Before pushing code and opening a PR in Azure DevOps:
  - Ensure the solution builds successfully without warnings.
  - Run all local unit and integration tests.
  - Remove any debugging logs, temporary configuration values, or draft code.
- **Link Work Items**: Always associate the PR with the corresponding work items on Azure Boards.

### Azure Pipelines (CI/CD)
- **Build Validation**: The Azure Pipeline should run automatically on PR creation and updates.
- **Pipeline Structure**: Build configuration must follow a clean path:
  1. **Restore**: Restore NuGet dependencies.
  2. **Build**: Compile the application in Release mode.
  3. **Test**: Run all automated unit and integration tests.
  4. **Publish**: Publish the output compilation files as pipeline artifacts.
  5. **Deploy**: Prepare artifacts for deployment to staging/production.
- **Do Not Rush Deployment**: Keep the deployment step basic initially. Do not introduce complex multi-stage deployment steps or infrastructure-as-code complexity before the core application code is stable.
- **Configuration Security**: Use Azure Pipelines Variable Groups or Azure Key Vault to pass database passwords, API keys, or connection strings. Never commit secrets directly to repository files.

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
