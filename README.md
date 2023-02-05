# TestsExtensions

[![main](https://github.com/Nairda015/TestsExtensions/actions/workflows/main.yml/badge.svg)](https://github.com/Nairda015/TestsExtensions/actions/workflows/main.yml)


## JsonData use case
Tests with lots of complicated scenarios can produce thousands of lines of code with tests setup also it can be hard to maintain.  
With this library, you can use json as tests input and based on the test method signature we will generate the required files to deserialize json without bloating your code.
```cs
public class ChartTests
{
    [JsonTheory<IMarker>]
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
You can pass an array of objects inside one file or add multiple attributes with the correct path to test cases.  
Json syntax should align with method sygnature.
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
