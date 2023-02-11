namespace TestsExtensions.Internal.Exceptions;

public sealed class FileIsEmptyException : TestsExtensionsException
{
    public FileIsEmptyException(string filePath) 
        : base($"File {filePath} is empty")
    {
    }
}