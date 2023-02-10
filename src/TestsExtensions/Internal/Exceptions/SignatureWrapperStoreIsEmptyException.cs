namespace TestsExtensions.Internal.Exceptions;

internal sealed class SignatureWrapperStoreIsEmptyException : TestsExtensionsException
{
    public SignatureWrapperStoreIsEmptyException()
        : base("SignatureWrapperStore is empty")
    {
    }
}