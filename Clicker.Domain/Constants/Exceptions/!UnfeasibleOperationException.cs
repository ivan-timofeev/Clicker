namespace Clicker.Domain.Constants.Exceptions;

public abstract class UnfeasibleOperationException : Exception
{
    public abstract string ExceptionSystemName { get; }

    public IDictionary<string, string> Details { get; }
        = new Dictionary<string, string>();

    protected UnfeasibleOperationException(string message)
        : base(message)
    {
        
    }
}
