namespace GenWrap.Abstraction.Internal.Exceptions;

internal sealed class AssemblyScanningException : GenWrapException
{
    public AssemblyScanningException()
        : base("Scanning for test objects failed - check if your marker is in correct assembly")
    {
    }
}