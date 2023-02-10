namespace TestsExtensions.Internal.Exceptions;

internal sealed class AssemblyScanningException : TestsExtensionsException
{
    public AssemblyScanningException()
        : base("Scanning for test objects failed - check if your marker is in correct assembly")
    {
    }
}