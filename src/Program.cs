using CardActionService.Infrastructure.Data;
using CardActionService.Infrastructure.Services;
using CardActionService.Infrastructure.Middleware;
using CardActionService.Application.Interfaces;
using CardActionService.Configuration.Logging;
using CardActionService.Domain.Providers;
using CorrelationId.DependencyInjection;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;
using Serilog;

LoggingSetup.ConfigureLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                // Dev
                "https://localhost:5173",
                // Prod
                "https://millenium.bank.example.pl"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDefaultCorrelationId(options =>
{
    options.AddToLoggingScope = true;
    options.EnforceHeader = false;
    options.IncludeInResponse = true;
});

builder.Host.UseSerilog();
builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0); //  Default API version
    options.ReportApiVersions = true; // Add header
    
    // Optional: Enforce API versioning via custom header
    // This requires consumers to specify the version in the request header (e.g. "X-API-Version: 1").
    // Useful when building long-term maintainable APIs with multiple versions in parallel.
    // Example request:
    // UserId: User2
    // CardNumber: Card212
    // X-API-Version: 1
    // options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var environment = builder.Environment.EnvironmentName;

switch (environment)
{
    case "Development":
        builder.Services.AddSingleton<ICardDataProvider, SampleCardDataProvider>();
        break;

    // Examples below demonstrate how provided architecture
    // is designed to accommodate different data providers.

    case "Staging":
        builder.Services.AddScoped<ICardDataProvider, SqlCardDataProvider>(); // Placeholder
        break;

    case "Production":
        builder.Services.AddScoped<ICardDataProvider, KafkaCardDataProvider>(); // Placeholder
        break;

    default:
        throw new Exception($"Unsupported environment: {environment}");
}

builder.Services.AddScoped<ICardResponseFactory, CardResponseFactory>();
builder.Services.AddSingleton<IMatrixProvider, MatrixProvider>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddSingleton<CardResolver>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Card Action Service API";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Card Action API v1");
    });
}


app.UseHttpsRedirection();
app.UseCorrelationId();
app.UseCors("AllowFrontend");
app.UseSerilogRequestLogging();
app.UseMiddleware<CorrelationIdLoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
// app.UseAuthentication();
app.UseAuthorization();
// app.UseRouting();
app.MapControllers();

app.Run();