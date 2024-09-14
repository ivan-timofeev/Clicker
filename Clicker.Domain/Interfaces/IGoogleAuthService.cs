using Clicker.Domain.Entities;

namespace Clicker.Domain.Interfaces;

public interface IGoogleAuthService
{
    public Task<User> AuthenticateAsync(string googleUserId, CancellationToken cancellationToken);
}
