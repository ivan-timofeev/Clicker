using Clicker.Domain.Constants.Exceptions;
using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;
using MediatR;

namespace Clicker.Application.Features;

public class ProcessUserClicksSequenceRequest : IRequest
{
    public User User { get; }
    public int ClicksQuantity { get; }

    public ProcessUserClicksSequenceRequest(User user, int clicksQuantity)
    {
        ArgumentNullException.ThrowIfNull(user);
        User = user;

        if (clicksQuantity < 0)
        {
            throw new WrongInputDataException(
                "ClicksQuantity must be a positive number.",
                nameof(clicksQuantity),
                clicksQuantity.ToString());
        }
        ClicksQuantity = clicksQuantity;
    }
}

public class ProcessUserClicksSequenceRequestHandler : IRequestHandler<ProcessUserClicksSequenceRequest>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMediator _mediator;

    public ProcessUserClicksSequenceRequestHandler(
        IUsersRepository usersRepository,
        IMediator mediator)
    {
        _usersRepository = usersRepository;
        _mediator = mediator;
    }

    public Task Handle(ProcessUserClicksSequenceRequest request, CancellationToken cancellationToken)
    {
        var user = request.User;
        var clicksCount = request.ClicksQuantity;

        var multiplier = 1M;
        var result = user.Balance;

        for (var i = 0; i < clicksCount; i++)
        {
            result += 1 * multiplier;
            multiplier += 0.01M;
            multiplier = Math.Min(multiplier, 10M);
        }

        user.Balance = result;
        return Task.CompletedTask;
    }
}
