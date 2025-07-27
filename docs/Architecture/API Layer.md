# API Layer

- CardController handles a single endpoint:
  **GET /api/v1/Card?UserId=abc&CardNumber=xyz**

- Request is validated
- Card data is fetched via injected provider
- Logic resolved via CardResolver
- Output returned in JSON format

Headers:
```text
X-API-KEY: test-key
```
