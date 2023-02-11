using GenWrap.Abstraction.Internal.Extensions;

namespace GenWrap.UnitTests.Internal.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("a", "A")]
    [InlineData("A", "A")]
    [InlineData("test", "Test")]
    [InlineData("testData", "TestData")]
    [InlineData("TestData", "TestData")]
    public void ToCamelCase_ShouldReturnStringInCorrectFormat(string input, string expected)
    {
        var actual = input.ToCamelCase();
        Assert.Equal(expected, actual);
    }
}