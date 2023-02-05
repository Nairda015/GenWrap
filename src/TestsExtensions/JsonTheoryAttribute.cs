using Xunit;

namespace TestsExtensions;

/// <summary>
/// This is entry point for source generator
/// </summary>
public class JsonTheoryAttribute<TMarker> : TheoryAttribute
{
    public JsonTheoryAttribute() => JsonDataAttribute.ScanAssembly<TMarker>();
    
}

/// <summary>
/// This is entry point for source generator
/// </summary>
public class JsonTheoryAttribute : TheoryAttribute
{
    public JsonTheoryAttribute(Type marker) => JsonDataAttribute.ScanAssembly(marker.Assembly);
}