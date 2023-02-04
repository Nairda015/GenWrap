using FluentAssertions;
using TestsExtensions.Examples.CalculatorExample;

namespace TestsExtensions.Examples.xUnit.CalculatorExample;

public class CalculatorTest
{
    [JsonTheory]
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