using CardActionService.Application.Interfaces;
using CardActionService.Infrastructure.Data;
using CardActionService.Infrastructure.Services;

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

    case "Staging":
        builder.Services.AddScoped<ICardDataProvider, SqlCardDataProvider>(); // Placeholder
        break;

    case "Production":
        builder.Services.AddScoped<ICardDataProvider, KafkaCardDataProvider>(); // Placeholder
        break;

    default:
        throw new Exception($"Unsupported environment: {environment}");
}

builder.Services.AddScoped<ICardService, CardService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();