using System.Text.Json;

namespace TestsExtensions.Examples.xUnit.Generated;

// Example of generated class for Calculator_Add test
file class TestObject : ITestObject
{
    public int InputA { get; set; } = default!;
    public int InputB { get; set; } = default!;
    public int Expected { get; set; } = default!;
    public IEnumerable<object[]> Deserialize(string json)
    {
        var datalist = JsonSerializer.Deserialize<List<TestObject>>(json);
        if (datalist is null) return new List<object[]>();

        return datalist
            .Select(data => new object[] { data.InputA, data.InputB, data.Expected }) //order matters
            .ToList();
    }
}