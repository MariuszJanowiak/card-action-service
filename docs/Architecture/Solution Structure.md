# Solution Structure

## /src
- Api (Controllers, Requests, Responses)
- Application (Contracts, Interfaces)
- Domain (Enums, Models, Parsers, Providers, Validators, Mappers, Exceptions)
- Infrastructure (Data Providers, Services, Middleware, Mappers)
- Configuration (API behavior, Logging, Security, Swagger)
- Properties (launchSettings.json)

## /tests
- Unit and integration tests for all features and middlewares

## /docs
- MkDocs documentation (Markdown files, config, custom CSS)

## Root files
- Program.cs (entry point)
- appsettings.*.json (environment config)
- Dockerfile, compose.yaml (containerization)

Environment-specific providers (e.g., CardDataProvider) are injected at runtime using configuration.

