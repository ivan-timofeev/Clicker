using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Clicker.Infrastructure.Services;

public class DatabaseSeeder
{
    private readonly ILogger<DatabaseSeeder> _logger;
    private readonly IUsersRepository _usersRepository;

    public DatabaseSeeder(
        ILogger<DatabaseSeeder> logger,
        IUsersRepository usersRepository)
    {
        _logger = logger;
        _usersRepository = usersRepository;
    }

    public async Task SeedAsync()
    {
        _logger.LogInformation("Database seeding...");

        for (var i = 0; i < 25000; i++)
        {
            var user = new User
            {
                Id = (i + 1).ToString(),
                Login = $"{Faker.Name.Last()}{Random.Shared.Next(1960, 2004)}@{Faker.Internet.DomainName()}",
                Balance = Random.Shared.Next(1000, 250000)
            };

            await _usersRepository.AddUserAsync(user, CancellationToken.None);
        }
        
        _logger.LogInformation("Database successfully seeded");
    }
}
