namespace Clicker.Api.Models.UserAuthenticator;

public class GoogleUserAuthenticatorDto : UserAuthenticatorDtoBase
{
    public required string GoogleUserId { get; init; }
}
