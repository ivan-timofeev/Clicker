using Clicker.Application.Dto;
using Clicker.Application.Features;
using Clicker.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clicker.Api.Controllers;

[ApiController]
[Route("api/users/{userId}/clicks")]
public class UsersClicksController : Controller
{
    private readonly IUsersRepository _usersRepository;

    public UsersClicksController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [HttpPost]
    public async Task ProcessClickSequence(
        [FromRoute] string userId,
        [FromBody] int userClicks,
        CancellationToken cancellationToken)
    {
        var user = await GetUser.Create(_usersRepository).ExecuteAsync(userId, cancellationToken);

        await ProcessUserClicksSequence
            .Create(_usersRepository)
            .ExecuteAsync(new ProcessUserClicksSequenceRequest { User = user, UserClicks = userClicks }, cancellationToken);
    }
}
