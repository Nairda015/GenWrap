using FluentAssertions;
using GenWrap.Examples.CalculatorExample;
using GenWrap.xUnit;
using Xunit;

namespace GenWrap.Examples.xUnit.CalculatorExample;

public class CalculatorTests
{
    [Theory]
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