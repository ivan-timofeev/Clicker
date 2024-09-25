using Clicker.Api.Attributes;
using Clicker.Api.Services;
using Clicker.Domain.Constants.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Clicker.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthPlaygroundController : Controller
{
    private readonly JwtService _jwtService;

    public AuthPlaygroundController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpGet("test")]
    [AuthorizedUserRequired]
    public IActionResult TestAuth([FromAuthorization] string userId)
    {
        return Ok(new { UserId = userId });
    }

    [HttpGet("test-permission")]
    public void TestPermission(string permission)
    {
        _jwtService
            .Authorize(HttpContext)
            .EnsureUserHavePermission(permission);
    }

    [HttpPost("create-jwt")]
    [PermissionRequired(Permissions.Jwt.Create)]
    public IActionResult CreateJwt(CreateJwtRequest createJwtRequest)
    {
        var jwt = _jwtService.CreateJwtToken(
            createJwtRequest.UserId,
            createJwtRequest.Role,
            createJwtRequest.Permissions,
            createJwtRequest.ExpiresAfter);

        return Ok(new { Jwt = jwt });
    }

    public record CreateJwtRequest(DateTime ExpiresAfter, string UserId, string Role, string[] Permissions);
}
