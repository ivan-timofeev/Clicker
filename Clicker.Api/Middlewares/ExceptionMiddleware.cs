using System.Text.Json;
using Clicker.Domain.Constants.Exceptions;

namespace Clicker.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (UnfeasibleOperationException exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errorResponse = new
            {
                exception.Message,
                exception.Details
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await httpContext.Response.WriteAsync(jsonResponse);
        }
        catch (ArgumentException exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errorResponse = new
            {
                exception.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await httpContext.Response.WriteAsync(jsonResponse);
        }
        catch
        {
            await _next(httpContext);
        }
    }
}
