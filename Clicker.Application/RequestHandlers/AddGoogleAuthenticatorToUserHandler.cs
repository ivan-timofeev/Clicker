using Clicker.Application.Builder;
using Clicker.Application.Requests;
using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.RequestHandlers;

public class AddGoogleAuthenticatorToUserHandler : IRequestHandler<AddGoogleAuthenticatorToUserRequest>
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
            authenticator.GoogleUserId = googleUserId;
            return Task.CompletedTask;
        }

        user.Authenticators = new AuthenticatorsBuilder()
            .AddGoogleAuthenticator(googleUserId)
            .Build();
        return Task.CompletedTask;
    }
}
