using System.Reflection;
using System.Text.Json;
using TestsExtensions.Examples.ChartExample;
using TestsExtensions.Extensions;
using Xunit.Sdk;

namespace TestsExtensions.Examples.xUnit.ChartExample;

// Example of generated attribute for Chart_SimplifyPriceChangedSet test
public class JsonData2Attribute : DataAttribute
{
    private readonly string _filePath;

    public JsonData2Attribute(string filePath) => _filePath = filePath;

    /// <inheritDoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));
        
        var fileData = PathExtension.GetJsonFileData(_filePath);
        if (string.IsNullOrWhiteSpace(fileData)) return new List<object[]>();
        
        var datalist = JsonSerializer.Deserialize<List<TestObject>>(fileData);
        if (datalist is null) return new List<object[]>();

        return datalist
            .Select(data => new object[] { data.Events, data.Expected }) //order matters
            .ToList();
    }

    private class TestObject
    {
        public List<PriceChangedEvent> Events { get; set; } = default!;
        public List<ChartPoint> Expected { get; set; } = default!;
    }
}
