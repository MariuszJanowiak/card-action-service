using CardActionService.Infrastructure.Data;
using CardActionService.Infrastructure.Services;
using CardActionService.Infrastructure.Middleware;
using CardActionService.Application.Interfaces;
using CardActionService.Domain.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
// app.UseAuthentication();
app.UseAuthorization();
// app.UseRouting();
app.MapControllers();

app.Run();