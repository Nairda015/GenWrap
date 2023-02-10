using FluentAssertions;
using TestsExtensions.Internal.Exceptions;
using TestsExtensions.Internal.Extensions;

namespace TestsExtensions.UnitTests.Internal.Extensions;

public class PathExtensionTests
{
    [Theory]
    [InlineData("TestData/simple.json")]
    [InlineData("./TestData/simple.json")]
    public void GetJsonFileData_ShouldReturnJsonContentAsString_WhenPathIsValid(string path)
    {
        // Arrange

        // Act
        var result = path.GetJsonFileData();

        // Assert
        result.Should().Be("[ 1, 2, 3 ]");
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("/TestData/simple.json")]
    [InlineData("~/TestData/simple.json")]
    public void GetJsonFileData_ShouldThrowPathIsMissingException_WhenPathIsInvalid(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.GetJsonFileData);

        // Assert
        exception.Should().BeOfType<PathIsMissingException>();
        exception!.Message.Should().Be($"Could not find file at path: {path}");
    }
    
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetJsonFileData_ShouldThrowArgumentNullException_WhenPathIsInvalid(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.GetJsonFileData);

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }
}