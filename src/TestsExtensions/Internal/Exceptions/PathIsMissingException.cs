namespace TestsExtensions.Internal.Exceptions;

internal class PathIsMissingException : TestsExtensionsException
{
    public PathIsMissingException(string message) : base(message)
    {
    }
}