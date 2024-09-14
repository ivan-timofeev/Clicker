namespace Clicker.Domain.Constants.Exceptions;

public class UserNotFoundException : UnfeasibleOperationException
{
    public override string ExceptionSystemName => "UserNotFound";

    public UserNotFoundException(string userId)
        : base("User with provided userId not found.")
    {
        Details.Add("userId", userId);
    }
}
