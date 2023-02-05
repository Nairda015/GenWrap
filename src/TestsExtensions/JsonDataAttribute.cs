using System.Diagnostics;
using System.Reflection;
using TestsExtensions.Extensions;
using Xunit.Sdk;

namespace TestsExtensions;

/// <summary>
/// This is entry point for source generator
/// </summary>
public class JsonDataAttribute : DataAttribute
{
    private readonly string _filePath;
    private static Dictionary<string, ITestObject> _testObjects = new();

    public JsonDataAttribute(string filePath) => _filePath = filePath;

    /// <inheritDoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));

        var fileData = PathExtension.GetJsonFileData(_filePath);
        if (string.IsNullOrWhiteSpace(fileData)) return new List<object[]>();

        return _testObjects.TryGetValue(_filePath, out var testObject)
            ? testObject.Deserialize(fileData)
            : throw new UnreachableException("Scanning for test objects failed");
    }

    //Probably collection with test objects should be stored in separate class
    public static void ScanAssembly<TMarker>()
    {
        if (_testObjects.Count > 0) return;
        
        var assembly = Assembly.GetAssembly(typeof(TMarker))!;
        ScanAssembly(assembly);
    }
    
    public static void ScanAssembly(Assembly assembly) 
        => _testObjects = assembly
            .GetTypes()
            .Where(x => typeof(ITestObject).IsAssignableFrom(x) && x.IsClass)
            .Select(Activator.CreateInstance)
            .Cast<ITestObject>()
            .ToDictionary(o => o.Key, x => x);
}