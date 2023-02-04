using FluentAssertions;
using TestsExtensions.Examples.ChartExample;

namespace TestsExtensions.Examples.xUnit.ChartExample;

public class ChartTests
{
    [JsonTheory]
    [JsonData2("ChartExample/TestData/Chart_SimplifyPriceChangedSet.json")]
    public void SimplifyPriceChangedSet_ShouldReturnSimplifyChartPoints(List<PriceChangedEvent> events, List<ChartPoint> expected)
    {
        // Arrange
        var calculator = new Chart();
        
        // Act
        var result = calculator.SimplifyPriceChangedSet(events);
        
        // Assert
        result.Count.Should().Be(expected.Count);
        result.Should().BeEquivalentTo(expected);
    }
}