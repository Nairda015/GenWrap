using FluentAssertions;
using GenWrap.Examples.CalculatorExample;
using GenWrap.NUnit;
using NUnit.Framework;

namespace GenWrap.Examples.NUnit.CalculatorExample;

public class CalculatorTests
{
    [Test]
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