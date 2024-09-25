using Clicker.Api.Extensions;
using Clicker.Api.Services;
using Clicker.Application.Features;
using Clicker.Domain.Constants.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Clicker.Api.Controllers.Auth;

[ApiController]
[Route("api/auth/google")]
public class GoogleAuthController : Controller
{
    private readonly IMediator _mediator;
    private readonly JwtService _jwtService;

    public GoogleAuthController(IMediator mediator, JwtService jwtService)
    {
        _mediator = mediator;
        _jwtService = jwtService;
    }
    
    [HttpGet("login")]
    public IActionResult LoginGoogle()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback") };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("signin")]
    public async Task<string> GoogleCallback(CancellationToken cancellationToken)
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (!authenticateResult.Succeeded)
        {
            throw new AuthenticationException(); 
        }

        var googleUserId = authenticateResult.GetGoogleUserId();
        var user = await _mediator.Send(new AuthenticateRequest(googleUserId), cancellationToken);

        return _jwtService.CreateJwtToken(user.Id, "User", permissions: [], DateTime.UtcNow.Date.AddDays(128));
    }
}
