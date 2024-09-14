using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Requests;

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
