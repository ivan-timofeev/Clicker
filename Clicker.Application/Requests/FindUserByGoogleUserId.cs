using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Requests;

public class FindUserByGoogleUserId : IRequest<User?>
{
    public string GoogleUserId { get; }

    public FindUserByGoogleUserId(string googleUserId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(googleUserId);
        GoogleUserId = googleUserId;
    }
}
