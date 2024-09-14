using Clicker.Domain.Constants.Exceptions;
using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;

namespace Clicker.Infrastructure.Data;

public class InMemoryUsersRepository : IUsersRepository
{
    private readonly InMemoryUsersStorage _inMemoryUsersStorage;

    public InMemoryUsersRepository(InMemoryUsersStorage inMemoryUsersStorage)
    {
        _inMemoryUsersStorage = inMemoryUsersStorage;
    }
    
    public Task AddUserAsync(User user, CancellationToken cancellationToken)
    {
        _inMemoryUsersStorage.Users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User> GetUserAsync(string userId, CancellationToken cancellationToken)
    {
        var isUserFound = _inMemoryUsersStorage.Users.Any(k => k.Id == userId);
        if (!isUserFound)
        {
            throw new UserNotFoundException(userId);
        }
        
        return Task.FromResult(_inMemoryUsersStorage.Users.Single(u => u.Id == userId));
    }

    public Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        var isUserFound = _inMemoryUsersStorage.Users.Any(k => k.Id == user.Id);
        if (!isUserFound)
        {
            throw new UserNotFoundException(user.Id);
        }
        
        var foundUser = _inMemoryUsersStorage.Users.Single(u => u.Id == user.Id);
        foundUser.Balance = user.Balance;
        
        return Task.CompletedTask;
    }

    public IQueryable<User> Users => _inMemoryUsersStorage.Users.AsQueryable();
}
