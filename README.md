# GenWrap

[![CI](https://github.com/Nairda015/GenWrap/actions/workflows/pull-request.yml/badge.svg)](https://github.com/Nairda015/GenWrap/actions/workflows/pull-request.yml)
[![codecov](https://codecov.io/gh/Nairda015/GenWrap/branch/main/graph/badge.svg?token=DAD5PSBP23)](https://codecov.io/gh/Nairda015/GenWrap)
[![NuGet Badge](https://buildstats.info/nuget/GenWrap.Abstraction)](https://www.nuget.org/packages/GenWrap.Abstraction/)

## JsonData use case
Tests with lots of complicated scenarios can produce thousands of lines of code with tests setup also it can be hard to maintain.  
With this library, you can use json as tests input and based on the test method signature we will generate the required files to deserialize json without bloating your code.

### xUnit
```cs
public class ChartTests
{
    [Theory]
    [JsonData("ChartExample/TestData/Chart_SimplifyPriceChangedSet.json")]
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
```

### NUnit
```cs
public class ChartTests
{
    [Test]
    [JsonData("ChartExample/TestData/Chart_SimplifyPriceChangedSet.json")]
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
```

### JSON
```json
[
  {
    "Events": [
      { "Price": 10, "DateTime": "2023-01-01T18:47:11.0000000+00:00" },
      { "Price": 11, "DateTime": "2023-02-01T18:47:11.0000000+00:00" },
      { "Price": 12, "DateTime": "2023-03-01T18:47:11.0000000+00:00" },
      { "Price": 10, "DateTime": "2022-01-01T18:47:11.0000000+00:00" },
      { "Price": 20, "DateTime": "2022-02-01T18:47:11.0000000+00:00" },
      { "Price": 15, "DateTime": "2021-01-01T18:47:11.0000000+00:00" },
      { "Price": 25, "DateTime": "2021-02-01T18:47:11.0000000+00:00" }
    ],
    "Expected": [
      { "X" : 2023, "Y" : 11 },
      { "X" : 2022, "Y" : 15 },
      { "X" : 2021, "Y" : 20 }
    ]
  },
  {
    "Events": [
      { "Price": 10.2, "DateTime": "2023-01-01T18:47:11.0000000+00:00" },
      { "Price": 11.3, "DateTime": "2023-02-01T18:47:11.0000000+00:00" },
      { "Price": 12.4, "DateTime": "2023-03-01T18:47:11.0000000+00:00" },
      { "Price": 10.5, "DateTime": "2022-01-01T18:47:11.0000000+00:00" },
      { "Price": 20.5, "DateTime": "2022-02-01T18:47:11.0000000+00:00" },
      { "Price": 15.3, "DateTime": "2021-01-01T18:47:11.0000000+00:00" },
      { "Price": 25.7, "DateTime": "2021-02-01T18:47:11.0000000+00:00" }
    ],
    "Expected": [
      { "X" : 2023, "Y" : 11.3 },
      { "X" : 2022, "Y" : 15.5 },
      { "X" : 2021, "Y" : 20.5 }
    ]
  }
]
```
You can pass an array of objects inside one file or add multiple attributes with the correct path to test cases.  
Json syntax should align with method signature.
