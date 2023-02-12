using System.Reflection;
using GenWrap.Abstraction.Internal;
using GenWrap.Abstraction.Internal.Exceptions;
using GenWrap.Abstraction.Internal.Extensions;
using Xunit.Sdk;

namespace GenWrap.xUnit;

/// <summary>
/// This is entry point for source generator
/// </summary>
public class JsonDataAttribute : DataAttribute
{
    private readonly string _filePath;
    public JsonDataAttribute(string filePath) => _filePath = filePath;

    /// <inheritDoc />
    public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
    {
        if (SignatureWrapperStore.IsEmpty())
        {
            SignatureWrapperStore.ScanAssembly(methodInfo.DeclaringType!.Assembly);
        }
        
        var fileData = _filePath.GetJsonFileData();
        if (string.IsNullOrWhiteSpace(fileData)) throw new FileIsEmptyException(_filePath);

        return SignatureWrapperStore.TryGetValue(_filePath, out var wrapper)
            ? wrapper!.Deserialize(fileData)
            : throw new AssemblyScanningException();
    }
}