using Clicker.Domain.Constants.Exceptions;
using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;
using MediatR;

namespace Clicker.Application.Features;

public class CreateUserRequest : IRequest<User>
{
    public string Login { get; }
    public string? VisibleName { get; init; }
    public string? Email { get; init; }

    public CreateUserRequest(string login)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(login);
        Login = login;
    }
}

public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, User>
{
    private readonly IUserLoginAvailabilityChecker _userLoginAvailabilityChecker;
    private readonly IUsersRepository _usersRepository;

    public CreateUserRequestHandler(
        IUserLoginAvailabilityChecker userLoginAvailabilityChecker,
        IUsersRepository usersRepository)
    {
        _userLoginAvailabilityChecker = userLoginAvailabilityChecker;
        _usersRepository = usersRepository;
    }

    public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
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
            Balance = 0,
            UserPersonalData = new UserPersonalData
            {
                Name = request.VisibleName,
                Email = request.Email
            }
        };

        await _usersRepository.AddUserAsync(user, cancellationToken);
        return user;
    }
}
