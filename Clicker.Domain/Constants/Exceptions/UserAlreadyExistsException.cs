namespace Clicker.Domain.Constants.Exceptions;

public class UserAlreadyExistsException : UnfeasibleOperationException
{
    public override string ExceptionSystemName => "UserAlreadyExists";

    public UserAlreadyExistsException(string googleUserId)
        : base("User with provided googleUserId already exists.")
    {
        Details.Add("googleUserId", googleUserId);
    }
}
