using System.Security.Claims;

namespace Clicker.Domain.Constants;

public static class SecurityExtensions
{
    public static string GetClaimValue(this ClaimsPrincipal principal, string claimType)
    {
        return principal.Claims.Single(c => c.Type == claimType).Value;
    }

    public static string? FindClaimValue(this ClaimsPrincipal principal, string claimType)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }
}
