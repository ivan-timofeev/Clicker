using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;
using MediatR;

namespace Clicker.Application.Features;

public class GetUserRequest : IRequest<User>
{
    public string UserId { get; }

    public GetUserRequest(string userId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);
        UserId = userId;
    }
}


public class GetUserRequestHandler : IRequestHandler<GetUserRequest, User>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserRequestHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        return _usersRepository.GetUserAsync(request.UserId, cancellationToken);
    }
}