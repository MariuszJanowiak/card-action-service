# FAQ

**Q: How do I change or add matrix rules?**  
A: Edit the rules in `Domain/Providers/MatrixProvider.cs` or extend the logic in `CardResolver`. All matrix logic is separated and fully testable.

**Q: Where do I configure the data source?**  
A: Data providers are in `Infrastructure/Data`. By default, the project uses `SampleCardDataProvider`. You can swap to SQL or Kafka by changing DI or configuration.

**Q: How does API authentication work?**  
A: Every API call requires the `X-API-KEY` header. Requests without a valid key return HTTP 401 Unauthorized.

**Q: How do I add a new card action or status?**  
A: Extend the appropriate enums in `Domain/Enums` and update the matrix rules in `MatrixProvider.cs`.

**Q: How is environment-specific behavior handled?**  
A: The project uses environment variables and `appsettings.*.json` files to inject different data providers and settings at runtime.

**Q: How do I enable or change logging?**  
A: Logging is configured via Serilog in `Program.cs` and can be customized in the `appsettings.*.json` files.

**Q: What should I change for production deployment?**  
A: Use a real data provider, configure `appsettings.Production.json`, set a secure API key, and configure monitoring/logging if needed.

**Q: How is error handling managed?**  
A: All errors are returned using standardized `ProblemDetails` responses via custom middleware.

**Q: How do I update API versioning?**  
A: API versioning is set up in the controllers and Swagger configuration. Update versions in the route attributes and Swagger as needed.

