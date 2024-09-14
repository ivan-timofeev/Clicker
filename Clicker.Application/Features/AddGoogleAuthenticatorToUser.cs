using Clicker.Application.Builders;
using Clicker.Domain.Entities;

namespace Clicker.Application.Features;

public class AddGoogleAuthenticatorToUser
{
    public Task ExecuteAsync(User user, string googleUserId)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(googleUserId);

        var authenticator = user
            .Authenticators
            .OfType<GoogleUserAuthenticator>()
            .SingleOrDefault(userGoogleAuthenticator => userGoogleAuthenticator.GoogleUserId == googleUserId);

        if (authenticator != default)
        {
            authenticator.GoogleUserId = googleUserId;
            return Task.CompletedTask;
        }

        user.Authenticators = new AuthenticatorsBuilder()
            .AddGoogleAuthenticator(googleUserId)
            .Build();
        return Task.CompletedTask;
    }

    public static AddGoogleAuthenticatorToUser Create()
    {
        return new AddGoogleAuthenticatorToUser();
    }
}