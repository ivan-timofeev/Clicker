using Microsoft.AspNetCore.Authentication;

namespace Clicker.Api.Extensions;

public static class AuthenticateResultExtensions
{
    public static string GetGoogleUserId(this AuthenticateResult authenticateResult)
    {
        ArgumentNullException.ThrowIfNull(authenticateResult);
        ArgumentNullException.ThrowIfNull(authenticateResult.Ticket);
        ArgumentNullException.ThrowIfNull(authenticateResult.Ticket.Principal);
        ArgumentNullException.ThrowIfNull(authenticateResult.Ticket.Principal.Claims);

        return authenticateResult
            .Ticket
            .Principal
            .Claims
            .Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            .Value;
    }
}
