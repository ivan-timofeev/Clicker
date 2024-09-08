using Clicker.Application.Dto;
using Clicker.Domain.Constants.Exceptions;
using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;

namespace Clicker.Application.Features;

public class CreateUser
{
    private readonly IUserLoginAvailabilityChecker _userLoginAvailabilityChecker;
    private readonly IUsersRepository _usersRepository;

    private CreateUser(
        IUserLoginAvailabilityChecker userLoginAvailabilityChecker,
        IUsersRepository usersRepository)
    {
        _userLoginAvailabilityChecker = userLoginAvailabilityChecker;
        _usersRepository = usersRepository;
    }
    
    public async Task<User> ExecuteAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var isLoginAvailable = await _userLoginAvailabilityChecker.IsLoginAvailableAsync(request.Login, cancellationToken);

        if (!isLoginAvailable)
        {
            throw new LoginIsNotAvailableException(request.Login);
        }

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Login = request.Login,
            Balance = 0
        };

        await _usersRepository.SaveUserAsync(user, cancellationToken);
        return user;
    }

    public static CreateUser Create(
        IUserLoginAvailabilityChecker userLoginAvailabilityChecker,
        IUsersRepository usersRepository)
    {
        ArgumentNullException.ThrowIfNull(userLoginAvailabilityChecker);
        ArgumentNullException.ThrowIfNull(usersRepository);
        return new CreateUser(userLoginAvailabilityChecker, usersRepository);
    }
}
