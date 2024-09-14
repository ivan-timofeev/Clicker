using Clicker.Api.Models;
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
    public async Task<UserDto> GetUserAsync([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var user = await GetUser
            .Create(_usersRepository)
            .ExecuteAsync(userId, cancellationToken);

        return UserDto.FromModel(user);
    }

    [HttpPost]
    public async Task<UserDto> CreateUserAsync([FromBody] CreateUserRequest createUserRequest, CancellationToken cancellationToken)
    {
        var createdUser = await CreateUser
            .Create(_userLoginAvailabilityChecker, _usersRepository)
            .ExecuteAsync(createUserRequest, cancellationToken);

        return UserDto.FromModel(createdUser);
    }
}
