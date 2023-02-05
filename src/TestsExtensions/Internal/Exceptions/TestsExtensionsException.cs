namespace TestsExtensions.Internal.Exceptions;

public abstract class TestsExtensionsException : Exception
{
    protected TestsExtensionsException(string message) : base(message)
    {
    }
}