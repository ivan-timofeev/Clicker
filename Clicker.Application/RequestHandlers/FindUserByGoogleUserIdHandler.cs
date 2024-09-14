using Clicker.Application.Requests;
using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;
using MediatR;

namespace Clicker.Application.RequestHandlers;

public class FindUserByGoogleUserIdHandler : IRequestHandler<FindUserByGoogleUserId, User?>
{
    private readonly IUsersRepository _usersRepository;

    public FindUserByGoogleUserIdHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public Task<User?> Handle(FindUserByGoogleUserId request, CancellationToken cancellationToken)
    {
        var user = _usersRepository.Users.SingleOrDefault(u =>
            u.Authenticators
                .OfType<GoogleUserAuthenticator>()
                .Any(userGoogleAuth => userGoogleAuth.GoogleUserId == request.GoogleUserId));

        return Task.FromResult(user);
    }
}
