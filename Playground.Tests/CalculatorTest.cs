using Xunit;

namespace Playground.Tests;

public class CalculatorTest
{
    [Theory]
    [JsonFileData<MathInputGrouped, MathResult>("TestData/Calculator_Add.json")]
    public void Test1(MathInputGrouped input, MathResult expected)
    {
        // Arrange
        var calculator = new Calculator();
        
        // Act
        var result = calculator.Add(input.Input1, input.Input2);
        
        // Assert
        Assert.Equal(expected.Result, result.Result);
    }

    public record MathInputGrouped(MathInput Input1, MathInput Input2);
}