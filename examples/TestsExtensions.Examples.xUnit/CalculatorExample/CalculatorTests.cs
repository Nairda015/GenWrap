using FluentAssertions;
using TestsExtensions.Examples.CalculatorExample;
using Xunit;

namespace TestsExtensions.Examples.xUnit.CalculatorExample;

public class CalculatorTests
{
    [JsonTheory<IMarker>]
    [JsonData("CalculatorExample/TestData/Calculator_Add.json")]
    public void Add_ShouldReturnCorrectResult(int inputA, int inputB, int expected)
    {
        // Arrange
        var calculator = new Calculator();
        
        // Act
        var result = calculator.Add(inputA, inputB);
        
        // Assert
        result.Should().Be(expected);
    }
}