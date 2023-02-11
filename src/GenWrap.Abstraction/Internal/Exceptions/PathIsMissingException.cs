namespace GenWrap.Abstraction.Internal.Exceptions;

internal sealed class PathIsMissingException : GenWrapException
{
    public PathIsMissingException(string path)
        : base($"Could not find file at path: {path}")
    {
    }
}