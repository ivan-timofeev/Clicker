using Clicker.Application.Requests;
using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.RequestHandlers;

public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, User>
{
    private readonly IMediator _mediator;

    public AuthenticateHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<User> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new FindUserByGoogleUserId(request.GoogleUserId), cancellationToken);
        if (user != default)
        {
            return user;
        }

        var createdUser = await _mediator.Send(new CreateUserRequest($"google-{request.GoogleUserId}"), cancellationToken);
        await _mediator.Send(new AddGoogleAuthenticatorToUserRequest(createdUser, request.GoogleUserId), cancellationToken);
        return createdUser;
    }
}
