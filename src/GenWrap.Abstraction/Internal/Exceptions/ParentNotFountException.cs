namespace GenWrap.Abstraction.Internal.Exceptions;

internal sealed class ParentNotFountException<T> : GenWrapException
{
    public ParentNotFountException()
        : base($"Parent {typeof(T).Name} not found")
    {
    }
}