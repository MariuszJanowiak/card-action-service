using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CardActionService.Configuration.Swagger;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "X-API-KEY",
            Type = SecuritySchemeType.ApiKey,
            Description = "API key required to access endpoints"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    }
                },
                Array.Empty<string>()
            }
        });

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = $"Card Action Service API",
                Version = description.ApiVersion.ToString(),
                Description = "API responsible for resolving allowed actions for a given bank card",
                Contact = new OpenApiContact
                {
                    Name = "Documentation",
                    Url = new Uri("https://github.com/MariuszJanowiak")
                }
            });
        }
    }

}