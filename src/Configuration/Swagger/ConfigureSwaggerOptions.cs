using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CardActionService.Configuration.Swagger;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
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