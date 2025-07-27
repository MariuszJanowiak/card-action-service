using CardActionService.Configuration.Security;
using Microsoft.Extensions.Options;

namespace CardActionService.Infrastructure.Middleware;

public class ApiKeyMiddleware(RequestDelegate next, IOptions<KeyOption> options)
{
    private readonly string _apiKey = options.Value.ApiKey;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey) || extractedApiKey != _apiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await next(context);
    }
}