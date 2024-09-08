using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;

namespace Clicker.Application.Features;

public class GetUser
{
    private readonly IUsersRepository _usersRepository;

    private GetUser(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<User> ExecuteAsync(string userId, CancellationToken cancellationToken)
    {
        return _usersRepository.GetUserAsync(userId, cancellationToken);
    }

    public static GetUser Create(IUsersRepository usersRepository)
    {
        ArgumentNullException.ThrowIfNull(usersRepository);
        return new GetUser(usersRepository);
    }
}
