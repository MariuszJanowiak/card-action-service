# Helper Modules

- FluentValidation – request and model validation
- Serilog – structured logging for the whole application
- CorrelationId – adds and logs correlation IDs for every request
- IP Rate Limiting – restricts API usage per client IP

All modules are fully configured in `Program.cs`:
- Registered as middleware in the pipeline
- Logging and validation rules set up at startup
- Security (API key, rate limits) enforced before business logic

