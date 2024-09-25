using Clicker.Api.Attributes;
using Clicker.Api.Models;
using Clicker.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clicker.Api.Controllers;

[ApiController]
[Route("api/current-user")]
[AuthorizedUserRequired]
public class CurrentUserController : Controller
{
    private readonly IMediator _mediator;

    public CurrentUserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<UserDto> GetUserAsync(
        [FromAuthorization] string userId,
        CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserRequest(userId), cancellationToken);

        return UserDto.FromModel(user);
    }

    [HttpPost("process-click-sequence")]
    public async Task ProcessClickSequence(
        [FromAuthorization] string userId,
        [FromBody] int clicksQuantity,
        CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserRequest(userId), cancellationToken);
        await _mediator.Send(new ProcessUserClicksSequenceRequest(user, clicksQuantity), cancellationToken);
    }
}
