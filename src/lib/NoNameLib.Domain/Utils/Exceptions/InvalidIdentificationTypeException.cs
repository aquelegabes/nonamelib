namespace NoNameLib.Domain.Utils.Exceptions;
public class InvalidIdentificationTypeException : Exception
{
    public InvalidIdentificationTypeException()
    {
    }

    public InvalidIdentificationTypeException(string message) : base(message)
    {
    }

    public InvalidIdentificationTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}