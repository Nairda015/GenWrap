using System.Diagnostics.CodeAnalysis;

namespace GenWrap.Abstraction.Internal.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class GenWrapException : Exception
{
    protected GenWrapException(string message)
        : base(message)
    {
    }
}