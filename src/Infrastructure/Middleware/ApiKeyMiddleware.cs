namespace CardActionService.Infrastructure.Middleware;

public class ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
{
    public async Task Invoke(HttpContext context)
    {
        var requiredKey = config["Security:ApiKey"];
        if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey) || apiKey != requiredKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await next(context);
    }
}