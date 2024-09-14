using Clicker.Api.Extensions;
using Clicker.Api.Models;
using Clicker.Domain.Constants.Exceptions;
using Clicker.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Clicker.Api.Controllers.Auth;

[ApiController]
[Route("api/auth/google")]
public class UsersClicksController : Controller
{
    private readonly IGoogleAuthService _googleAuthService;

    public UsersClicksController(IGoogleAuthService googleAuthService)
    {
        _googleAuthService = googleAuthService;
    }
    
    [HttpGet("login")]
    public IActionResult LoginGoogle()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback") };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("signin")]
    public async Task<UserDto> GoogleCallback(CancellationToken cancellationToken)
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (!authenticateResult.Succeeded)
        {
            throw new AuthenticationException(); 
        }

        var googleUserId = authenticateResult.GetGoogleUserId();
        var user = await _googleAuthService.AuthenticateAsync(googleUserId, cancellationToken);

        return UserDto.FromModel(user);
    }
}
