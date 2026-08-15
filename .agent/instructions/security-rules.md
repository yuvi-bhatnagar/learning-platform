# Security Rules

- Use ASP.NET Core Identity for authentication.
- Use role/policy-based authorization.
- Never trust role information supplied by the client.
- Validate authorization on the server.
- Use antiforgery protection for applicable POST requests.
- Validate all user input.
- Never expose sensitive fields unnecessarily.
- Never hard-code passwords, tokens, connection strings, or secrets.
- Use configuration/environment variables for secrets.
- Prevent users from accessing another user's resources.
- Verify resource ownership inside the backend.
- Do not rely on hidden fields for authorization.
- Return appropriate HTTP responses for unauthorized access.