using Clicker.Domain.Entities;
using Clicker.Infrastructure.Data;

namespace Clicker.Api.Graphql.Queries;

public class FindUsersQuery
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> UsersQuery([Service] InMemoryUsersStorage context)
    {
        return context.Users.AsQueryable();
    }
}
