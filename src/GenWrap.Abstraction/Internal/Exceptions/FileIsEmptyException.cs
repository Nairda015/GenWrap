namespace GenWrap.Abstraction.Internal.Exceptions;

internal sealed class FileIsEmptyException : GenWrapException
{
    public FileIsEmptyException(string filePath) 
        : base($"File {filePath} is empty")
    {
    }
}