using Clicker.Application.Dto;
using Clicker.Application.Features;
using Clicker.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clicker.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : Controller
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserLoginAvailabilityChecker _userLoginAvailabilityChecker;

    public UsersController(
        IUsersRepository usersRepository,
        IUserLoginAvailabilityChecker userLoginAvailabilityChecker)
    {
        _usersRepository = usersRepository;
        _userLoginAvailabilityChecker = userLoginAvailabilityChecker;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserAsync([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var user = await GetUser.Create(_usersRepository).ExecuteAsync(userId, cancellationToken);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest createUserRequest, CancellationToken cancellationToken)
    {
        var createdUser = await CreateUser
            .Create(_userLoginAvailabilityChecker, _usersRepository)
            .ExecuteAsync(createUserRequest, cancellationToken);

        return Ok(createdUser);
    }
}
