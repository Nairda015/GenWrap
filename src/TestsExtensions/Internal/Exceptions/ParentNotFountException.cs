namespace TestsExtensions.Internal.Exceptions;

internal sealed class ParentNotFountException<T> : TestsExtensionsException
{
    public ParentNotFountException()
        : base($"Parent {typeof(T).Name} not found")
    {
    }
}