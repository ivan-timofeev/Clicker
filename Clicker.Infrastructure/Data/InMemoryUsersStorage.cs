using Clicker.Domain.Entities;

namespace Clicker.Infrastructure.Data;

public class InMemoryUsersStorage
{
    public List<User> Users { get; } = new(capacity: 2500);
}
