using System.Reflection;
using System.Text.Json;
using TestsExtensions.Extensions;
using Xunit.Sdk;

namespace TestsExtensions;

// Example of generated attribute for Calculator_Add test
public class JsonDataAttribute : DataAttribute
{
    private readonly string _filePath;

    public JsonDataAttribute(string filePath) => _filePath = filePath;

    /// <inheritDoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));
        
        var fileData = _filePath.GetJsonFileData();
        if (fileData is null) return new List<object[]>();
        
        var datalist = JsonSerializer.Deserialize<List<TestObject>>(fileData);
        if (datalist is null) return new List<object[]>();

        return datalist
            .Select(data => new object[] { data.InputA, data.InputB, data.Expected }) //order matters
            .ToList();
    }

    private class TestObject
    {
        public int InputA { get; set; } = default!;
        public int InputB { get; set; } = default!;
        public int Expected { get; set; } = default!;
    }
}
