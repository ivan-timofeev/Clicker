using Clicker.Domain.Entities;

namespace Clicker.Application.Dto;

public class UpdateUserBalanceRequest
{
    public required User User { get; init; }
    public required decimal Balance { get; init; }
}
