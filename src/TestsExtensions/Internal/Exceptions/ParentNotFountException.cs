namespace TestsExtensions.Internal.Exceptions;

internal class ParentNotFountException : TestsExtensionsException
{
    public ParentNotFountException(string message) : base(message)
    {
    }
}