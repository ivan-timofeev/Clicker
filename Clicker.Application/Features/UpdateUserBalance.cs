using Clicker.Application.Dto;
using Clicker.Domain.Interfaces;

namespace Clicker.Application.Features;

public class UpdateUserBalance
{
    private readonly IUsersRepository _usersRepository;

    private UpdateUserBalance(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task ExecuteAsync(UpdateUserBalanceRequest balanceRequest, CancellationToken cancellationToken)
    {
        balanceRequest.User.Balance = balanceRequest.Balance;
        await _usersRepository.UpdateUserAsync(balanceRequest.User, cancellationToken);
    }

    public static UpdateUserBalance Create(IUsersRepository usersRepository)
    {
        ArgumentNullException.ThrowIfNull(usersRepository);
        return new UpdateUserBalance(usersRepository);
    }
}
