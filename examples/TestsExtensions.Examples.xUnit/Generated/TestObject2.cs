using System.Text.Json;
using TestsExtensions.Examples.ChartExample;

namespace TestsExtensions.Examples.xUnit.Generated;

// Example of generated class for Chart_SimplifyPriceChangedSet test

file class TestObject2 : ITestObject
{
    public List<PriceChangedEvent> Events { get; set; } = default!;
    public List<ChartPoint> Expected { get; set; } = default!;
    
    public IEnumerable<object[]> Deserialize(string json)
    {
        var datalist = JsonSerializer.Deserialize<List<TestObject2>>(json);
        if (datalist is null) return new List<object[]>();

        return datalist
            .Select(data => new object[] { data.Events, data.Expected }) //order matters
            .ToList();
    }
}