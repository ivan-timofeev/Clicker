namespace Clicker.Domain.Constants.Exceptions;

public class AuthenticationException : UnfeasibleOperationException
{
    public override string ExceptionSystemName => "AuthenticationError";

    public AuthenticationException()
        : base("Authentication error. Please check your credentials and try again.")
    {
        
    }
}
