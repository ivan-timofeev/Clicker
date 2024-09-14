using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Requests;

public class AuthenticateRequest : IRequest<User>
{
    public string GoogleUserId { get; }

    public AuthenticateRequest(string googleUserId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(googleUserId);
        GoogleUserId = googleUserId;
    }
}
