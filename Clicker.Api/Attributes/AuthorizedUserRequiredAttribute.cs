using Clicker.Api.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clicker.Api.Attributes;

public class AuthorizedUserRequiredAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.ActionArguments["userId"] = context
            .HttpContext
            .RequestServices
            .GetRequiredService<JwtService>()
            .Authorize(context.HttpContext)
            .GetUserId();

        base.OnActionExecuting(context);
    }
}
