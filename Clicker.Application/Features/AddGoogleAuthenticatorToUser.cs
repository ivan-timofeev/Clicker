using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Features;

public class AddGoogleAuthenticatorToUserRequest : IRequest
{
    public User User { get; }
    public string GoogleUserId { get; }

    public AddGoogleAuthenticatorToUserRequest(User user, string googleUserId)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentException.ThrowIfNullOrWhiteSpace(googleUserId);

        User = user;
        GoogleUserId = googleUserId;
    }
}

public class AddGoogleAuthenticatorToUserRequestHandler : IRequestHandler<AddGoogleAuthenticatorToUserRequest>
{
    public Task Handle(AddGoogleAuthenticatorToUserRequest request, CancellationToken cancellationToken)
    {
        var user = request.User;
        var googleUserId = request.GoogleUserId;

        var authenticator = user
            .Authenticators
            .OfType<GoogleUserAuthenticator>()
            .SingleOrDefault(userGoogleAuthenticator => userGoogleAuthenticator.GoogleUserId == googleUserId);

        if (authenticator != default)
        {
            authenticator.SetGoogleUserId(googleUserId);
            return Task.CompletedTask;
        }

        user.Authenticators.Add(new GoogleUserAuthenticator(googleUserId));

        return Task.CompletedTask;
    }
}
