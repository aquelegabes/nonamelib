namespace NoNameLib.Application.Exceptions;

public class HandlerNotImplementedException : Exception
{
    public HandlerNotImplementedException() : base()
    {
    }

    public HandlerNotImplementedException(string? message) : base(message)
    {
    }

    public HandlerNotImplementedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
