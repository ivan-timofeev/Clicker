using Clicker.Domain.Interfaces;
using Clicker.Infrastructure.Data;
using Clicker.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Clicker.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<InMemoryUsersStorage>()
            .AddTransient<IUsersRepository, InMemoryUsersRepository>()
            .AddTransient<IUserLoginAvailabilityChecker, InMemoryUserLoginAvailabilityChecker>()
            .AddTransient<IGoogleAuthService, GoogleAuthService>();

        return serviceCollection;
    }

    public static IServiceCollection AddDatabaseSeeder(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<DatabaseSeeder>();

        return serviceCollection;
    }

    public static async Task UseDatabaseSeederAsync(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<DatabaseSeeder>().SeedAsync();
    }
}
