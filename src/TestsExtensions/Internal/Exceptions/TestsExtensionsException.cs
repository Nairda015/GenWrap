using System.Diagnostics.CodeAnalysis;

namespace TestsExtensions.Internal.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class TestsExtensionsException : Exception
{
    protected TestsExtensionsException(string message)
        : base(message)
    {
    }
}