using System.Diagnostics.CodeAnalysis;

namespace TestsExtensions.Examples.CalculatorExample;

[ExcludeFromCodeCoverage]
public class Calculator
{
    public int Add(int a, int b)
    {
        return  a + b;
    }
}