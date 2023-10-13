public class MessageBrokerUnhandledExceptionHolder
{
    private readonly Exception _currentException;
    public MessageBrokerUnhandledExceptionHolder(Exception exc)
    {
        _currentException = exc;
    }
    public Exception ExceptionDetails
    {
        get { return _currentException; }
    }
}