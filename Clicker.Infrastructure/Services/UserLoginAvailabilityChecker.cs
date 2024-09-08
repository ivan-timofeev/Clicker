using Clicker.Domain.Interfaces;
using Clicker.Infrastructure.Data;

namespace Clicker.Infrastructure.Services;

public class InMemoryUserLoginAvailabilityChecker : IUserLoginAvailabilityChecker
{
    private readonly InMemoryUsersStorage _inMemoryUsersStorage;

    public InMemoryUserLoginAvailabilityChecker(InMemoryUsersStorage inMemoryUsersStorage)
    {
        _inMemoryUsersStorage = inMemoryUsersStorage;
    }
    
    public Task<bool> IsLoginAvailableAsync(string login, CancellationToken cancellationToken)
    {
        var isLoginAvailable = _inMemoryUsersStorage.Users.All(k => k.Login != login);
        return Task.FromResult(isLoginAvailable);
    }
}
