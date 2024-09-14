using Clicker.Application.Requests;
using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;
using MediatR;

namespace Clicker.Application.RequestHandlers;

public class GetUserHandler : IRequestHandler<GetUserRequest, User>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        return _usersRepository.GetUserAsync(request.UserId, cancellationToken);
    }
}