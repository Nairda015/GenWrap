using System.Diagnostics;
using System.Reflection;
using TestsExtensions.Extensions;
using TestsExtensions.Internal;
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
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));

        var fileData = _filePath.GetJsonFileData();
        if (string.IsNullOrWhiteSpace(fileData)) return new List<object[]>();

        var dict = new Dictionary<int, int>();

        return SignatureWrapperStore.TryGetValue(_filePath, out var testObject)
            ? testObject.Deserialize(fileData)
            : throw new UnreachableException("Scanning for test objects failed");
    }
}