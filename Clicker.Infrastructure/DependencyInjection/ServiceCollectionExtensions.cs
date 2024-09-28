using Clicker.Domain.Interfaces;
using Clicker.Infrastructure.Data;
using Clicker.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clicker.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<InMemoryUsersStorage>()
            .AddTransient<IUsersRepository, InMemoryUsersRepository>()
            .AddTransient<IUserLoginAvailabilityChecker, InMemoryUserLoginAvailabilityChecker>();

        return serviceCollection;
    }
}
