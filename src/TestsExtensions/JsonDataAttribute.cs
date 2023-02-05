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
    private static readonly Dictionary<string, ITestObject> TestObjects = new();

    public JsonDataAttribute(string filePath) => _filePath = filePath;

    /// <inheritDoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));

        var fileData = PathExtension.GetJsonFileData(_filePath);
        if (string.IsNullOrWhiteSpace(fileData)) return new List<object[]>();

        return TestObjects.TryGetValue(_filePath, out var testObject)
            ? testObject.Deserialize(fileData)
            : throw new UnreachableException("Scanning for test objects failed");
    }

    //Probably collection with test objects should be stored in separate class
    public static void ScanAssembly<TMarker>()
    {
        if (TestObjects.Count > 0) return;
        
        var assembly = Assembly.GetAssembly(typeof(TMarker))!;
        ScanAssembly(assembly);
    }
    
    public static void ScanAssembly(Assembly assembly)
    {
        var methodsWithJsonDataAttribute = assembly
            .GetTypes()
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(typeof(JsonDataAttribute), false).Length > 0)
            .ToList();

        var signatures = methodsWithJsonDataAttribute
            .Select(GetParameterData);
        
        var attributes = methodsWithJsonDataAttribute
            .Select(x => x.GetCustomAttribute(typeof(JsonDataAttribute), false))
            .ToList();
       
        //TODO: fix this
        var parameters = attributes
            .Select(x => x!.GetType().GetField("_filePath")?.GetValue(x) as string);
        
        var elementsForSearching = parameters
            .Zip(signatures, (x, y) => new { Path = x, ParameterData = y })
            .ToList();

        var generatedTypes = assembly
            .GetTypes()
            .Where(x => typeof(ITestObject).IsAssignableFrom(x) && x.IsClass)
            .Select(Activator.CreateInstance)
            .Cast<ITestObject>()
            .ToDictionary(o => GetKeyForParameterData(GetParameterData(o)), x => x);
        
        //match generated type with method parameters
        foreach (var el in elementsForSearching)
        {
            if(el.Path is null) throw new UnreachableException("Path is null");
            if (generatedTypes.TryGetValue(GetKeyForParameterData(el.ParameterData), out var generatedType))
            {
                TestObjects.Add(el.Path!, generatedType);
            }

            throw new Exception("No generated type found");
        }
        
        //_testObjects.Add("CalculatorExample/TestData/Calculator_Add.json", new TestObject());
        //_testObjects.Add("ChartExample/TestData/Chart_SimplifyPriceChangedSet.json", new TestObject2());
    }

    private static string GetKeyForParameterData(IEnumerable<ParameterData> parameterData)
        => string.Join(";", parameterData
            .OrderBy(x => x.Name)
            .Select(x => $"{x.Name}:{x.Type}".ToLower()));
    
    private static List<ParameterData> GetParameterData(ITestObject generatedType) 
        => generatedType
            .GetType()
            .GetProperties()
            .Select(x => new ParameterData(x.Name, x.PropertyType))
            .ToList();

    private static List<ParameterData> GetParameterData(MethodBase methodInfo) 
        => methodInfo
            .GetParameters()
            .Select(x => new ParameterData(x.Name, x.ParameterType))
            .ToList();
}