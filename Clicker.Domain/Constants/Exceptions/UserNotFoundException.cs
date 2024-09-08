namespace Clicker.Domain.Constants.Exceptions;

public class UserNotFoundException : UnfeasibleOperationException
{
    public string UserId { get; }

    public UserNotFoundException(string userId)
    {
        UserId = userId;
    }
}
