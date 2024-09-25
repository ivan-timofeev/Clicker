using Clicker.Api.Controllers.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace Clicker.Api.SwaggerExamples;

public class CreateJwtRequestExample : IExamplesProvider<AuthPlaygroundController.CreateJwtRequest>
{
    public AuthPlaygroundController.CreateJwtRequest GetExamples()
    {
        return new AuthPlaygroundController.CreateJwtRequest(
            DateTime.UtcNow.AddYears(1),
            "some-user-id",
            Role: "Admin",
            Permissions: ["Jwt:Create", "Users:Read", "Users:Write"]);
    }
}
