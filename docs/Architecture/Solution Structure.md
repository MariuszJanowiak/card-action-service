# Solution Structure

- `/src` — Main application
  - Controllers
  - Middleware
  - Services
  - Configuration
- `/tests` — Unit + Integration tests
- `/docs` — MkDocs documentation
- `Program.cs` — Entry point

Environment-specific behavior (e.g., CardDataProvider) is injected at runtime.
