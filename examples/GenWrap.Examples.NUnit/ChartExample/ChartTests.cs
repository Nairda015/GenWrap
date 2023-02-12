using FluentAssertions;
using GenWrap.Examples.ChartExample;
using GenWrap.NUnit;
using NUnit.Framework;

namespace GenWrap.Examples.NUnit.ChartExample;

public class ChartTests
{
    [Test]
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