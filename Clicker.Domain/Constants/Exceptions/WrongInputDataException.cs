namespace Clicker.Domain.Constants.Exceptions;

public class WrongInputDataException : UnfeasibleOperationException
{
    public WrongInputDataException(string message, string key, string value)
        : base(message)
    {
        Details.Add(key, value);
    }

    public override string ExceptionSystemName => "WrongInputDataException";
}
