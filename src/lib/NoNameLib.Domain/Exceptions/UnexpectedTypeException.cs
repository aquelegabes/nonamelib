namespace NoNameLib.Domain.Exceptions;

public class UnexpectedTypeException : Exception
{
    public UnexpectedTypeException() : base()
    {
    }

    public UnexpectedTypeException(string message) : base(message)
    {
    }

    public UnexpectedTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
