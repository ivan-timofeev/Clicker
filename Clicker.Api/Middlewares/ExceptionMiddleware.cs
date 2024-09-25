using System.Text.Json;
using Clicker.Domain.Constants.Exceptions;

namespace Clicker.Api.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (UnfeasibleOperationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errorResponse = new
            {
                exception.Message,
                exception.Details
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (ArgumentException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errorResponse = new
            {
                exception.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (UnauthorizedAccessException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            var errorResponse = new
            {
                exception.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch
        {
            await next(context);
        }
    }
}
