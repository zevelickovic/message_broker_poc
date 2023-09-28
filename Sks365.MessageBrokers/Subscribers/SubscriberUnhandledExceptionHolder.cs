public class SubscriberUnhandledExceptionHolder
{
    private readonly Exception _currentException;
    public SubscriberUnhandledExceptionHolder(Exception exc)
    {
        _currentException = exc;
    }
    public Exception ExceptionDetails
    {
        get { return _currentException; }
    }
}