using Clicker.Application.Dto;
using Clicker.Application.Features;
using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;

namespace Clicker.Infrastructure.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly IUserLoginAvailabilityChecker _userLoginAvailabilityChecker;
    private readonly IUsersRepository _usersRepository;

    public GoogleAuthService(
        IUserLoginAvailabilityChecker userLoginAvailabilityChecker,
        IUsersRepository usersRepository)
    {
        _userLoginAvailabilityChecker = userLoginAvailabilityChecker;
        _usersRepository = usersRepository;
    }

    public async Task<User> AuthenticateAsync(string googleUserId, CancellationToken cancellationToken)
    {
        var user = _usersRepository.Users.SingleOrDefault(u =>
            u.Authenticators
                .OfType<GoogleUserAuthenticator>()
                .Any(userGoogleAuth => userGoogleAuth.GoogleUserId == googleUserId));

        if (user != default)
        {
            return user;
        }
        
        user = await CreateUser
            .Create(_userLoginAvailabilityChecker, _usersRepository)
            .ExecuteAsync(new CreateUserRequest { Login = googleUserId }, cancellationToken);

        await AddGoogleAuthenticatorToUser
            .Create()
            .ExecuteAsync(user, googleUserId);

        return user;
    }
}
