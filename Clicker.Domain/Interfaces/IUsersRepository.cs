using Clicker.Domain.Constants.Exceptions;
using Clicker.Domain.Entities;

namespace Clicker.Domain.Interfaces;

public interface IUsersRepository
{
    Task AddUserAsync(User user, CancellationToken cancellationToken);
    
    /// <summary>
    /// Asynchronously retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the user object 
    /// if found; otherwise, it throws <c>UserNotFoundException</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="userId"/> is <c>null</c> or empty.</exception>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via the <paramref name="cancellationToken"/>.</exception>
    /// <exception cref="UserNotFoundException">Thrown when the user is not found.</exception>
    Task<User> GetUserAsync(string userId, CancellationToken cancellationToken);
    
    Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    
    IQueryable<User> Users { get; }
}
