namespace Clicker.Domain.Constants.Exceptions;

public class LoginIsNotAvailableException : UnfeasibleOperationException
{
    public string Login { get; }

    public LoginIsNotAvailableException(string login)
    {
        Login = login;
    }
}
