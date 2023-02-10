namespace TestsExtensions.Internal.Exceptions;

internal sealed class PathIsMissingException : TestsExtensionsException
{
    public PathIsMissingException(string path)
        : base($"Could not find file at path: {path}")
    {
    }
}