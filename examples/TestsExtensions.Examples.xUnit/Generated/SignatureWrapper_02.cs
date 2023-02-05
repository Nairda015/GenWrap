using System.Text.Json;
using TestsExtensions.Examples.ChartExample;

namespace TestsExtensions.Examples.xUnit.Generated;

// Example of generated class for Chart_SimplifyPriceChangedSet test

file record SignatureWrapper : ISignatureWrapper
{
    public string Key => "ChartExample/TestData/Chart_SimplifyPriceChangedSet.json";
    public List<PriceChangedEvent> Events { get; init; } = default!;
    public List<ChartPoint> Expected { get; init; } = default!;

    public IEnumerable<object[]> Deserialize(string json)
    {
        var datalist = JsonSerializer.Deserialize<List<SignatureWrapper>>(json);
        if (datalist is null) return new List<object[]>();

        return datalist
            .Select(data => new object[] { data.Events, data.Expected }) //order matters
            .ToList();
    }
}