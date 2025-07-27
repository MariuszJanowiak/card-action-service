using AspNetCoreRateLimit;
using CardActionService.Api.Requests;
using CardActionService.Application.Interfaces;
using CardActionService.Configuration;
using CardActionService.Configuration.Logging;
using CardActionService.Configuration.Swagger;
using CardActionService.Domain.Providers;
using CardActionService.Infrastructure.Data;
using CardActionService.Infrastructure.Middleware;
using CardActionService.Infrastructure.Services;
using CorrelationId;
using CorrelationId.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

Log.Logger = LoggingSetup.ConfigureLogger();

var builder = WebApplication.CreateBuilder(args);

#region Logging & Host

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

#endregion

#region CORS

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

#endregion

#region Correlation ID

builder.Services.AddDefaultCorrelationId(options =>
{
    options.AddToLoggingScope = true;
    options.EnforceHeader = false;
    options.IncludeInResponse = true;
});

#endregion

#region Controllers & Validation

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CardRequestValidator>();

builder.Services.ConfigureApiBehaviorOptions(builder.Environment);

#endregion

#region API Versioning & Swagger

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    // options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

#endregion

#region Dependency Injection - Environment

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

#endregion

#region Dependency Injection - Services

builder.Services.AddSingleton<IMatrixProvider, MatrixProvider>();
builder.Services.AddSingleton<ICardResolver, CardResolver>();

builder.Services.AddScoped<ICardResponseFactory, CardResponseFactory>();
builder.Services.AddScoped<ICardService, CardService>();

#endregion

#region Rate Limiting

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

#endregion

#region Build App

var app = builder.Build();

#endregion

#region Middleware Pipeline

#region Swagger Middleware (Development)

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

#endregion

app.UseHttpsRedirection();
app.UseRouting();

app.UseCorrelationId();
app.UseMiddleware<CorrelationIdLoggingMiddleware>();
app.UseSerilogRequestLogging();
app.UseCors("AllowFrontend");

app.UseIpRateLimiting();

app.UseMiddleware<IssueHandlingMiddleware>();

app.UseMiddleware<ApiKeyMiddleware>();

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
    await next();
});

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
