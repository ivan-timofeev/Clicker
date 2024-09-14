namespace Clicker.Domain.Constants.Exceptions;

public class LoginIsNotAvailableException : UnfeasibleOperationException
{
    public override string ExceptionSystemName => "LoginIsNotAvailable";

    public LoginIsNotAvailableException(string login)
        : base("Login is not available.")
    {
        Details.Add("login", login);
    }

}
