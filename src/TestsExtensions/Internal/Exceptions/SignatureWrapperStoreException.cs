namespace TestsExtensions.Internal.Exceptions;

internal sealed class SignatureWrapperStoreException : TestsExtensionsException
{
    public SignatureWrapperStoreException(string storeIsEmpty) : base(storeIsEmpty)
    {
    }
}