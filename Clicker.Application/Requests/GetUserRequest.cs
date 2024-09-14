using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Requests;

public class GetUserRequest : IRequest<User>
{
    public string UserId { get; }

    public GetUserRequest(string userId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);
        UserId = userId;
    }
}
