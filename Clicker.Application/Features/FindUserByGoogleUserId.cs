using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;
using MediatR;

namespace Clicker.Application.Features;

public class FindUserByGoogleUserIdRequest : IRequest<User?>
{
    public string GoogleUserId { get; }

    public FindUserByGoogleUserIdRequest(string googleUserId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(googleUserId);
        GoogleUserId = googleUserId;
    }
}

public class FindUserByGoogleUserIdRequestHandler : IRequestHandler<FindUserByGoogleUserIdRequest, User?>
{
    private readonly IUsersRepository _usersRepository;

    public FindUserByGoogleUserIdRequestHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public Task<User?> Handle(FindUserByGoogleUserIdRequest request, CancellationToken cancellationToken)
    {
        var user = _usersRepository.Users.SingleOrDefault(u =>
            u.Authenticators
                .OfType<GoogleUserAuthenticator>()
                .Any(userGoogleAuth => userGoogleAuth.GoogleUserId == request.GoogleUserId));

        return Task.FromResult(user);
    }
}
