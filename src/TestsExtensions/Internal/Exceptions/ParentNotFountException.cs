namespace TestsExtensions.Internal.Exceptions;

internal sealed class ParentNotFountException : TestsExtensionsException
{
    public ParentNotFountException(string message) : base(message)
    {
    }
}