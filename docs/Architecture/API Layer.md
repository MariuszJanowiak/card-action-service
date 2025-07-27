# API Layer

- `CardController` exposes a single HTTP GET endpoint:
  `/api/v1/Card?UserId=abc&CardNumber=xyz`

- Incoming request is validated via FluentValidation.
- Card data is fetched from an injected provider (SQL, Kafka, or Sample).
- Card actions are resolved through `CardResolver`.
- Response is returned in JSON array format (e.g., `["ACTION1", "ACTION3"]`).

Authorization header required:
```text
X-API-KEY: test-key

