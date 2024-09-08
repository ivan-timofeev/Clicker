using System.Numerics;

namespace Clicker.Application.Dto;

public class CreateUserRequest
{
    public required string Login { get; init; }
}