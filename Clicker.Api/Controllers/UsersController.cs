using Clicker.Api.Models;
using Clicker.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clicker.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : Controller
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    public async Task<UserDto> GetUserAsync(
        [FromRoute] string userId,
        CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserRequest(userId), cancellationToken);

        return UserDto.FromModel(user);
    }

    [HttpPost]
    public async Task<UserDto> CreateUserAsync(
        [FromBody] CreateUserRequest createUserRequest,
        CancellationToken cancellationToken)
    {
        var createdUser = await _mediator.Send(createUserRequest, cancellationToken);

        return UserDto.FromModel(createdUser);
    }
}
