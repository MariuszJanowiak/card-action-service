using System.Net;
using System.Text.Json;
using CardActionService.Domain.Exceptions;

namespace CardActionService.Infrastructure.Middleware;

public class IssueHandlingMiddleware(RequestDelegate next, ILogger<IssueHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            await IssueResponse(context, HttpStatusCode.Unauthorized, "Unauthorized", ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await IssueResponse(context, HttpStatusCode.NotFound, "Not Found", ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await IssueResponse(context, HttpStatusCode.BadRequest, "Bad Request", ex.Message);
        }
        catch (DomainValidationException ex)
        {
            await IssueResponse(context, HttpStatusCode.BadRequest, "Domain validation error", ex.Message);
        }
        catch (Exception ex)
        {
            await IssueResponse(context, HttpStatusCode.InternalServerError, "Internal Server Error", ex.Message);
        }
    }

    async private Task IssueResponse(HttpContext context, HttpStatusCode statusCode, string title, string detail)
    {
        if ((int)statusCode >= 500)
            logger.LogError("Exception handled in middleware: {Title} - {Detail}", title, detail);
        else
            logger.LogWarning("Exception handled in middleware: {Title} - {Detail}", title, detail);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var issue = new
        {
            type = $"https://httpstatuses.com/{(int)statusCode}",
            title,
            status = (int)statusCode,
            detail,
            instance = context.Request.Path
        };

        var json = JsonSerializer.Serialize(issue);
        await context.Response.WriteAsync(json);
    }
}
