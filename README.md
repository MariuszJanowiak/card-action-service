# Card Action Service API

Secure and extensible .NET 8 API for resolving permitted actions on banking cards (Credit, Debit, Prepaid).  
Designed with testability, clean architecture, and modern security practices in mind.

## Features

- Matrix-based rule evaluation engine
- Environment-aware data provider injection (Sample, SQL, Kafka-ready)
- Middleware for security, logging, correlation, and error handling
- Swagger UI for exploration and manual testing
- Ready for Docker and CI/CD pipelines

## Documentation

Full technical [documentation](https://MariuszJanowiak.github.io/card-action-service/) and additional [security tests](https://github.com/MariuszJanowiak/card-action-service-security) is available here.

## Getting Started

To run the API locally:

```bash
dotnet run --project src
