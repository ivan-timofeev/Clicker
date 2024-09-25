using Clicker.Api.Attributes;
using Clicker.Api.Services;

namespace Clicker.Api.Middlewares;

public class PermissionBasedAuthMiddleware : IMiddleware
{
    private readonly JwtService _jwtService;

    public PermissionBasedAuthMiddleware(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var permissionRequiredAttribute = context
            .GetEndpoint()?
            .Metadata
            .GetMetadata<PermissionRequiredAttribute>();

        if (permissionRequiredAttribute != null)
        {
            _jwtService
                .Authorize(context)
                .EnsureUserHavePermission(permissionRequiredAttribute.Permission);
        }

        await next(context);
    }
}
