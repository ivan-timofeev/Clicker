using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Requests;

public class CreateUserRequest : IRequest<User>
{
    public string Login { get; }

    public CreateUserRequest(string login)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(login);
        Login = login;
    }
}
