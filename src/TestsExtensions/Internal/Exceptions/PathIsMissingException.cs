namespace TestsExtensions.Internal.Exceptions;

internal sealed class PathIsMissingException : TestsExtensionsException
{
    public PathIsMissingException(string message) : base(message)
    {
    }
}