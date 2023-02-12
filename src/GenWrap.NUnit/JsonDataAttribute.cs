using GenWrap.Abstraction.Internal;
using GenWrap.Abstraction.Internal.Exceptions;
using GenWrap.Abstraction.Internal.Extensions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace GenWrap.NUnit;

/// <summary>
/// This is entry point for source generator
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class JsonDataAttribute : NUnitAttribute, ITestBuilder
{
    private readonly string _filePath;
    private readonly NUnitTestCaseBuilder _builder = new();
    public JsonDataAttribute(string filePath) => _filePath = filePath;

    /// <inheritDoc />
    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test? suite)
    {
        if (SignatureWrapperStore.IsEmpty())
        {
            SignatureWrapperStore.ScanAssembly(method.MethodInfo.DeclaringType!.Assembly);
        }
        
        var fileData = _filePath.GetJsonFileData();
        if (string.IsNullOrWhiteSpace(fileData)) throw new FileIsEmptyException(_filePath);
        
        var inputs =  SignatureWrapperStore.TryGetValue(_filePath, out var wrapper)
            ? wrapper!.Deserialize(fileData)
            : throw new AssemblyScanningException();

        return inputs.Select(x => new TestCaseParameters(x))
            .Select(x => _builder.BuildTestMethod(method, suite, x))
            .ToList();
    }
}
