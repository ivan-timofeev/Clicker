using System.Security.Claims;
using Clicker.Domain.Constants;
using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Features;

public class AuthenticateRequest : IRequest<User>
{
    public ClaimsPrincipal ClaimsPrincipal { get; }

    public AuthenticateRequest(ClaimsPrincipal сlaimsPrincipal)
    {
        ClaimsPrincipal = сlaimsPrincipal;
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
        var googleUserId = request.ClaimsPrincipal.GetClaimValue(Security.OAuth.UserIdClaim);

        var user = await _mediator.Send(new FindUserByGoogleUserIdRequest(googleUserId), cancellationToken);
        if (user != default)
        {
            return user;
        }

        var createdUser = await _mediator.Send(
            new CreateUserRequest(login: $"google-{googleUserId}")
            {
                Email = request.ClaimsPrincipal.FindClaimValue(Security.OAuth.EmailClaim),
                VisibleName = request.ClaimsPrincipal.FindClaimValue(Security.OAuth.NameClaim)
            },
            cancellationToken);

        await _mediator.Send(new AddGoogleAuthenticatorToUserRequest(createdUser, googleUserId), cancellationToken);

        return createdUser;
    }
}
