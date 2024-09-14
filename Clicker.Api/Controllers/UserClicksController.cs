using Clicker.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clicker.Api.Controllers;

[ApiController]
[Route("api/users/{userId}/clicks")]
public class UsersClicksController : Controller
{
    private readonly IMediator _mediator;

    public UsersClicksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task ProcessClickSequence(
        [FromRoute] string userId,
        [FromBody] int clicksQuantity,
        CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserRequest(userId), cancellationToken);
        await _mediator.Send(new ProcessUserClicksSequenceRequest(user, clicksQuantity), cancellationToken);
    }
}
