using Clicker.Application.Dto;
using Clicker.Domain.Interfaces;

namespace Clicker.Application.Features;

public class ProcessUserClicksSequence
{
    private readonly IUsersRepository _usersRepository;

    private ProcessUserClicksSequence(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task ExecuteAsync(ProcessUserClicksSequenceRequest request, CancellationToken cancellationToken)
    {
        var user = request.User;
        var clicksCount = request.UserClicks;

        var multiplier = 1M;
        var result = user.Balance;
        
        for (var i = 0; i < clicksCount; i++)
        {
            result += 1 * multiplier;
            multiplier += 0.01M;
            multiplier = Math.Min(multiplier, 10M);
        }

        var updateUserBalanceRequest = new UpdateUserBalanceRequest { Balance = result, User = user };
        await UpdateUserBalance
            .Create(_usersRepository)
            .ExecuteAsync(updateUserBalanceRequest, cancellationToken);
    }

    public static ProcessUserClicksSequence Create(IUsersRepository usersRepository)
    {
        ArgumentNullException.ThrowIfNull(usersRepository);
        return new ProcessUserClicksSequence(usersRepository);
    }
}
