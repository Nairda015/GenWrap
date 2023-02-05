using System.Diagnostics;
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
        if (string.IsNullOrWhiteSpace(fileData)) return new List<object[]>();

        return SignatureWrapperStore.TryGetValue(_filePath, out var testObject)
            ? testObject.Deserialize(fileData)
            : throw new AssemblyScanningException("Scanning for test objects failed");
    }
}