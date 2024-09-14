using Clicker.Domain.Constants.Exceptions;
using Clicker.Domain.Entities;
using MediatR;

namespace Clicker.Application.Requests;

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
