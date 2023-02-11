namespace GenWrap.Abstraction.Internal.Exceptions;

internal sealed class SignatureWrapperStoreIsEmptyException : GenWrapException
{
    public SignatureWrapperStoreIsEmptyException()
        : base("SignatureWrapperStore is empty")
    {
    }
}