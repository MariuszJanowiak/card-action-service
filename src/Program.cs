using AspNetCoreRateLimit;
using CardActionService.Infrastructure.Data;
using CardActionService.Infrastructure.Services;
using CardActionService.Infrastructure.Middleware;
using CardActionService.Application.Interfaces;
using CardActionService.Configuration.Logging;
using CardActionService.Configuration.Swagger;
using CardActionService.Domain.Providers;
using CorrelationId.DependencyInjection;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

Log.Logger = LoggingSetup.ConfigureLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "https://localhost:5001",
                "https://millenium.bank.example.pl")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Correlation ID
builder.Services.AddDefaultCorrelationId(options =>
{
    options.AddToLoggingScope = true;
    options.EnforceHeader = false;
    options.IncludeInResponse = true;
});

// Controllers
builder.Services.AddControllers();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    // options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

// Versioned Explorer
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

// Dependency Injection
var environment = builder.Environment.EnvironmentName;

switch (environment)
{
    case "Development":
        builder.Services.AddSingleton<ICardDataProvider, SampleCardDataProvider>();
        break;
    case "Staging":
        builder.Services.AddScoped<ICardDataProvider, SqlCardDataProvider>();
        break;
    case "Production":
        builder.Services.AddScoped<ICardDataProvider, KafkaCardDataProvider>();
        break;
    default:
        throw new Exception($"Unsupported environment: {environment}");
}

// Rate Limiting
builder.Services.AddScoped<ICardResponseFactory, CardResponseFactory>();
builder.Services.AddSingleton<IMatrixProvider, MatrixProvider>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddSingleton<CardResolver>();

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();



var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var apiVersionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in apiVersionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", 
                $"Card Action API {description.GroupName.ToUpper()}");
        }
    });
}

app.UseHttpsRedirection();
app.UseCorrelationId();
app.UseCors("AllowFrontend");
app.UseIpRateLimiting();
app.UseSerilogRequestLogging();
app.UseMiddleware<CorrelationIdLoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();