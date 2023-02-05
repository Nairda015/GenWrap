namespace TestsExtensions.Internal.Exceptions;

internal class SignatureWrapperStoreException : TestsExtensionsException
{
    public SignatureWrapperStoreException(string storeIsEmpty) : base(storeIsEmpty)
    {
    }
}