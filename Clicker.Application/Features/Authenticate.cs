using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Features;

public class AuthenticateRequest : IRequest<User>
{
    public string GoogleUserId { get; }

    public AuthenticateRequest(string googleUserId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(googleUserId);
        GoogleUserId = googleUserId;
    }
}

public class AuthenticateRequestHandler : IRequestHandler<AuthenticateRequest, User>
{
    private readonly IMediator _mediator;

    public AuthenticateRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<User> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new FindUserByGoogleUserIdRequest(request.GoogleUserId), cancellationToken);
        if (user != default)
        {
            return user;
        }

        var createdUser = await _mediator.Send(new CreateUserRequest($"google-{request.GoogleUserId}"), cancellationToken);
        await _mediator.Send(new AddGoogleAuthenticatorToUserRequest(createdUser, request.GoogleUserId), cancellationToken);
        return createdUser;
    }
}
