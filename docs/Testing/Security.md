# Security

### API Key Authentication

* Every request must include a valid `X-API-KEY` header.
* Requests without it return HTTP 401 Unauthorized.

### Middleware Safeguards

* Custom middleware validates the API key.
* `ProblemDetailsMiddleware` handles error formatting and HTTP codes.
* IP rate limiting prevents abuse and DDoS scenarios.
* `CorrelationIdMiddleware` assigns a unique ID per request for logging/tracing.
* HTTP headers are hardened with:

  * `X-Content-Type-Options: nosniff`
  * `X-Frame-Options: DENY`
  * `X-XSS-Protection: 1; mode=block`
  * `Strict-Transport-Security: max-age=31536000; includeSubDomains`
  * `Referrer-Policy: no-referrer`
  * `Permissions-Policy: geolocation=(), microphone=()`

### Testing Coverage

* Unit and integration tests validate both authorized and unauthorized access.
* API key checks and error responses are fully testable and verified.

### Example Header

```text
X-API-KEY: test-key
```
