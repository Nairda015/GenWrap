using GenWrap.Abstraction.Internal;
using Xunit;

namespace GenWrap.xUnit;

/// <summary>
/// This is entry point for scanning before tests run
/// </summary>
public class JsonTheoryAttribute<TMarker> : TheoryAttribute
{
    public JsonTheoryAttribute() => SignatureWrapperStore.ScanAssembly<TMarker>();
}

/// <summary>
/// This is entry point for scanning before tests run
/// </summary>
public class JsonTheoryAttribute : TheoryAttribute
{
    public JsonTheoryAttribute(Type marker) => SignatureWrapperStore.ScanAssembly(marker.Assembly);
}