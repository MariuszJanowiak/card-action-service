# Architecture

## API
- Controllers (CardController)
- Requests (input DTOs + validation)
- Responses (output DTOs)

## Application
- Contract (CardResponse, CardSummary)
- Interfaces (ICardService, ICardResolver, ICardDataProvider, ICardResponseFactory, IMatrixProvider)

## Domain
- Enums (EnActionFlag, EnCardType, EnCardStatus)
- Exceptions (DomainValidationException)
- Mappers (ActionFlagMapper)
- Models (CardDetails)
- Parsers (MatrixParser)
- Providers (MatrixProvider)
- Validators (MatrixValidator)

## Infrastructure
- Data providers (KafkaCardDataProvider, SqlCardDataProvider, SampleCardDataProvider + DTOs)
- Mappers (CardDataMapper)
- Middleware (ApiKeyMiddleware, CorrelationIdLoggingMiddleware, IssueHandlingMiddleware)
- Services (CardService, CardResolver, CardResponseFactory)

## Configuration
- API behavior (ConfigureApiBehavior)
- Logging (LoggingSetup, LogPathResolver)
- Security (KeyOption)
- Swagger (ConfigureSwaggerOptions)

## Testing
- `/tests` — unit and integration tests (CardResolverTests, ApiKeyMiddlewareTests, etc.)

## Other
- `appsettings.*.json` — environment config
- `launchSettings.json` — run/debug profiles
- `Dockerfile`, `compose.yaml` — containerization
- `Program.cs` — entry point

