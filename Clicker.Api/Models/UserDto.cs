using Clicker.Api.Models.UserAuthenticator;
using Clicker.Domain.Entities;

namespace Clicker.Api.Models;

public class UserDto
{
    public required string Id { get; init; }
    public required string Login { get; init; }
    public required decimal Balance { get; init; }
    public required IEnumerable<UserAuthenticatorDtoBase> Authenticators { get; init; }
    
    public static UserDto FromModel(User user)
    {
        var authenticatorsDto = user
            .Authenticators
            .OfType<GoogleUserAuthenticator>()
            .Select(g => new GoogleUserAuthenticatorDto { GoogleUserId = g.GoogleUserId })
            .ToArray();

        var userDto = new UserDto
        {
            Id = user.Id,
            Login = user.Login,
            Balance = user.Balance,
            Authenticators = authenticatorsDto
        };

        return userDto;
    }
}
