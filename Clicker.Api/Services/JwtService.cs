using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace Clicker.Api.Services;

public partial class JwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;

    public JwtService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"]
            ?? throw new InvalidOperationException("Secrets should contain Jwt:SecretKey.");
        _issuer = configuration["Jwt:Issuer"] 
            ?? throw new InvalidOperationException("Secrets should contain Jwt:Issuer.");
    }

    public string CreateJwtToken(string userId, string role, IEnumerable<string> permissions, DateTime expiresDateTimeUtc)
    {
        var signingKey = SHA256.HashData(Encoding.UTF8.GetBytes(_secretKey));

        var claims = new List<Claim>
        {
            new ("userId", userId),
            new ("role", role),
        };

        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(signingKey),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: "Any",
            claims: claims,
            expires: expiresDateTimeUtc,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal Authorize(HttpContext context)
    {
        var authorization = context.Request.Headers.Authorization.ToString();
        var regexMatch = BearerJwtTokenRegex().Match(authorization);

        return regexMatch.Success
            ? AuthorizeJwtToken(regexMatch.Groups[1].Value)
            : throw new ArgumentException("Authorization header must be like 'Bearer <JWT-TOKEN>'.");
    }

    private ClaimsPrincipal AuthorizeJwtToken(string jwt)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingKey = SHA256.HashData(Encoding.UTF8.GetBytes(_secretKey));
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };

            return tokenHandler.ValidateToken(jwt, validationParameters, out _);
        }
        catch
        {
            throw new UnauthorizedAccessException("Jwt token is not valid.");
        }
    }

    public static void EnsureUserHavePermission(ClaimsPrincipal claimsPrincipal, string requiredPermission)
    {
        var isUserHavePermission = claimsPrincipal
            .Claims
            .Where(c => c.Type == "permission")
            .Select(c => c.Value)
            .Contains(requiredPermission);

        if (!isUserHavePermission)
        {
            throw new UnauthorizedAccessException($"User does not have '{requiredPermission}' permission.");
        }
    }

    public static string GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal
            .Claims
            .SingleOrDefault(c => c.Type == "userId")
            ?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("Jwt token does not contain the 'userId' claim.");
        }

        return userId;
    }

    [GeneratedRegex("Bearer (.*)")]
    private static partial Regex BearerJwtTokenRegex();
}

public static class FluentInterface
{
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return JwtService.GetUserId(claimsPrincipal);
    }

    public static ClaimsPrincipal EnsureUserHavePermission(this ClaimsPrincipal claimsPrincipal, string requiredPermission)
    {
        JwtService.EnsureUserHavePermission(claimsPrincipal, requiredPermission);
        return claimsPrincipal;
    }
}
