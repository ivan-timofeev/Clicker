using Clicker.Application.Requests;
using Clicker.Domain.Interfaces;
using MediatR;

namespace Clicker.Application.RequestHandlers;

public class ProcessUserClicksSequenceHandler : IRequestHandler<ProcessUserClicksSequenceRequest>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMediator _mediator;

    public ProcessUserClicksSequenceHandler(
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
