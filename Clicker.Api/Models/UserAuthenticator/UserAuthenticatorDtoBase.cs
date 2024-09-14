using System.Text.Json.Serialization;

namespace Clicker.Api.Models.UserAuthenticator;

[JsonDerivedType(typeof(GoogleUserAuthenticatorDto), "google")]
public class UserAuthenticatorDtoBase
{
    
}
