namespace NoNameLib.Domain.Utils.Exceptions;

public class InvalidAttributeUsageException : Exception
{
    public InvalidAttributeUsageException() : base()
    {
    }

    public InvalidAttributeUsageException(string message) : base(message)
    {
    }

    public InvalidAttributeUsageException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
