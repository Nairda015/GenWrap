using System.Reflection;
using TestsExtensions.Internal;
using TestsExtensions.Internal.Exceptions;
using TestsExtensions.Internal.Extensions;
using Xunit.Sdk;

namespace TestsExtensions;

/// <summary>
/// This is entry point for source generator
/// </summary>
public class JsonDataAttribute : DataAttribute
{
    private readonly string _filePath;
    public JsonDataAttribute(string filePath) => _filePath = filePath;

    /// <inheritDoc />
    public override IEnumerable<object[]> GetData(MethodInfo _)
    {
        var fileData = _filePath.GetJsonFileData();
        if (string.IsNullOrWhiteSpace(fileData)) throw new FileIsEmptyException(_filePath);

        return SignatureWrapperStore.TryGetValue(_filePath, out var wrapper)
            ? wrapper!.Deserialize(fileData)
            : throw new AssemblyScanningException();
    }
}