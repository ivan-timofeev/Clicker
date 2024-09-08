namespace Clicker.Domain.Interfaces;

public interface IUserLoginAvailabilityChecker
{
    Task<bool> IsLoginAvailableAsync(string login, CancellationToken cancellationToken);
}
