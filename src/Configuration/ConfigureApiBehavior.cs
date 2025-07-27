using Microsoft.AspNetCore.Mvc;

namespace CardActionService.Configuration;

public static class ConfigureApiBehavior
{
    public static void ConfigureApiBehaviorOptions(this IServiceCollection services, IHostEnvironment env)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = env.IsDevelopment() ? "One or more validation errors occurred." : "Bad Request",
                    Detail = env.IsDevelopment() ? "See errors property for details." : null,
                    Instance = context.HttpContext.Request.Path
                };

                if (env.IsDevelopment())
                {
                    problemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                }

                return new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            };
        });
    }
}