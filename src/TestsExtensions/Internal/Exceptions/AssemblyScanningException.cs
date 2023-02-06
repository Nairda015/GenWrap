namespace TestsExtensions.Internal.Exceptions;

internal sealed class AssemblyScanningException : TestsExtensionsException
{
    public AssemblyScanningException(string message) : base(message)
    {
    }
}