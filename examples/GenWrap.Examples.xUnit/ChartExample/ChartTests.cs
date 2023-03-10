using FluentAssertions;
using GenWrap.Examples.ChartExample;
using GenWrap.xUnit;
using Xunit;

namespace GenWrap.Examples.xUnit.ChartExample;

public class ChartTests
{
    [Theory]
    [JsonData("ChartExample/TestData/Chart_SimplifyPriceChangedSet_01.json")]
    [JsonData("ChartExample/TestData/Chart_SimplifyPriceChangedSet_02.json")]
    public void SimplifyPriceChangedSet_ShouldReturnSimplifyChartPoints(
        List<PriceChangedEvent> events,
        List<ChartPoint> expected)
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